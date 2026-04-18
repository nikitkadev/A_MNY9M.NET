# Архитектурный гайд для Discord-бота (практичный Clean Architecture)

Дата: `2026-03-29`

## Короткий ответ на главный вопрос
Да, ты абсолютно прав: **чистая архитектура в интеграционном продукте не бывает «стерильной»**.
Для Discord-бота цель не в том, чтобы спрятать весь Discord.NET, а в том, чтобы:
1. Локализовать платформенную связанность в одном месте.
2. Не протаскивать типы Discord в use-cases/домен, где это не нужно.
3. Держать ядро бизнес-правил независимым от транспорта.

Иными словами: **не “чистота ради чистоты”, а контролируемая связанность**.

---

## Что уже хорошо в текущем проекте
- Слои разделены концептуально (`1_Domain`, `2_Application`, `3_Infrastructure`, `4_Presentation`).
- События Discord централизованно подписываются в одном сервисе.
- Команды и события уже заведены через MediatR, это хороший фундамент.

---

## Где сейчас архитектурный риск
Сейчас в Application-слое события и обработчики принимают напрямую `Socket*` типы из Discord.NET.
Из-за этого бизнес-логика жёстко сцеплена с транспортом/SDK.

Примеры:
- `SlashCommandExecuted` содержит `SocketSlashCommand`.
- `MessageReceived` содержит `SocketMessage`.
- В обработчике `SlashCommandExecutedHandler` есть Discord-specific `DeferAsync`, проверка channel id и парсинг options.

Это допустимо на старте, но тормозит масштабирование и тестирование.

---

## Рекомендуемая целевая модель (для твоего кейса)

### 1) Оставить Discord.NET в Presentation/Adapters
- Все `Socket*` объекты — только на границе.
- Там же ack/defer, ответы в Discord, форматирование Discord-специфичных сообщений.

### 2) Ввести application-контракты (DTO/Command)
Вместо `SocketSlashCommand` передавать внутрь:
- `ExecuteSlashCommandRequest` (plain C# модель):
  - `GuildId`, `UserId`, `ChannelId`, `CommandName`, `Options`, `TraceId`.

Вместо `SocketMessage`:
- `IncomingMessageEvent`:
  - `AuthorId`, `ChannelId`, `Content`, `AttachmentTypes`, `CreatedAtUtc`.

### 3) Anti-Corruption Layer (ACL) для Discord
Сделать mapper/translator:
- `DiscordSlashCommandMapper`
- `DiscordMessageMapper`

Он конвертирует `Socket*` → внутренние DTO.

### 4) Порт для исходящих действий
Чтобы use-case не знал о Discord.NET, добавить порт:
- `IPlatformResponder` / `IDiscordResponder` (Application interface)

Реализация в Infrastructure/Presentation отправляет фактический ответ в Discord.

---


## Цепочка маппинга (да, ты понял правильно)
1. `Discord API` → SDK (`Discord.NET`) уже превращает raw response в `Socket*` типы.
2. На границе приложения (adapter layer) ты делаешь второй маппинг: `Socket*` → твои `Application DTO/Command`.
3. Ниже (Application/Domain) работаешь только со своими моделями и правилами.
4. Ответ обратно: `Application Result` → adapter → Discord message/embed/component.

Это нормальная и правильная схема для интеграционных систем: двойной маппинг окупается тестируемостью, контролем границ и меньшей болью при изменениях SDK.

---

## Как разложить по отдельным .csproj (как ты и хочешь)

Рекомендованный минимум:

1. `Amnyam.Domain`
- Entities, ValueObjects, Domain errors/rules.
- Без Discord, EF, OpenAI.

2. `Amnyam.Application`
- Use-cases, команды, обработчики, интерфейсы-порты.
- Допустимы только абстракции и DTO.

3. `Amnyam.Infrastructure`
- EF Core, репозитории, OpenAI, кэш, файловые/JSON providers.

4. `Amnyam.Integrations.Discord` (или `Amnyam.Presentation.Discord`)
- Discord.NET client, listeners, mapping `Socket*` → application DTO.
- Отправка ответов в Discord.

5. `Amnyam.Host` (консоль/worker)
- DI composition root, запуск, конфиги окружения.

6. `Amnyam.Tests.Unit` и `Amnyam.Tests.Integration`
- Unit: Application/Domain без Discord SDK.
- Integration: Discord adapters, Infrastructure и БД сценарии.

---

## Практичное правило границ (очень важное)
Если класс импортирует `Discord.*`, он **не должен** содержать бизнес-правила.
Его задача — адаптация, валидация транспорта, маршрутизация и вызов application use-case.

Бизнес-решение “что делать” — в Application/Domain.
Решение “как отправить в Discord” — в adapters.

---

## Что делать пошагово (миграционный план без боли)

### Шаг 1 (1–2 дня)
- Ввести внутренние DTO для 2 горячих сценариев:
  - Slash command
  - Message received
- Сделать мапперы из `Socket*` в DTO.

### Шаг 2 (2–3 дня)
- Перенести из `SlashCommandExecutedHandler` Discord-specific куски на границу:
  - `DeferAsync`
  - Парсинг raw options
  - Discord-specific response formatting

### Шаг 3 (2–3 дня)
- Ввести порт `ICommandResponseGateway` (или `IInteractionResponder`).
- Реализацию оставить в Discord-адаптере.

### Шаг 4 (3–5 дней)
- Добавить unit-тесты на use-cases без Discord SDK.
- Минимум: `/set-voiceroom` и метрики сообщений.

### Шаг 5 (постепенно)
- Аналогично выносить остальные обработчики событий.
- Не переписывать всё разом, идти “vertical slices”.

---

## Как вызывать Discord.NET из Application, если Discord.NET там запрещён

Правильный путь: **через порты (интерфейсы) + адаптеры**.

### Шаблон
1. В `Application` объявляешь интерфейс (порт), например:
   - `IInteractionResponder`
   - `IGuildPlatformGateway`
2. В use-case/handler вызываешь **только интерфейс**.
3. Реализация интерфейса живёт в `Integrations.Discord` и уже использует `Discord.NET` (`Socket*`, `Rest*`, `EmbedBuilder` и т.д.).
4. DI в `Host` связывает интерфейс с Discord-реализацией.

### Мини-скелет
```csharp
// Application
public interface IInteractionResponder
{
    Task DeferAsync(InteractionRef interaction, bool ephemeral, CancellationToken ct);
    Task ReplyAsync(InteractionRef interaction, string text, CancellationToken ct);
}

public sealed record InteractionRef(string InteractionId, ulong UserId, ulong ChannelId);

public class SetupVoiceRoomHandler : IRequestHandler<SetupVoiceRoomCommand, Result>
{
    public async Task<Result> Handle(SetupVoiceRoomCommand cmd, CancellationToken ct)
    {
        // бизнес-логика
        // ...
        await responder.ReplyAsync(cmd.Interaction, "Готово", ct);
        return Result.Ok();
    }
}

// Integrations.Discord
public class DiscordInteractionResponder : IInteractionResponder
{
    // здесь Discord.NET и маппинг InteractionRef -> реальный interaction/context
}
```

### Важная мысль
`Application` **не обязан знать**, каким транспортом отправляется ответ.
Он только говорит: «нужно ответить пользователю». Как именно — забота адаптера.

### Где держать временный контекст interaction
Есть два рабочих варианта:
- Передавать в command lightweight-ссылку (`InteractionRef`) и хранить runtime-map в adapter-е.
- Делать ответ в Presentation после завершения use-case (handler вернул `Result`, adapter отправил message).

Оба варианта нормальные. Для начала проще второй (response orchestration в adapter).

---

## `ButtonExecuted`: где делать `DeferAsync` и как связать с MediatR

Для твоего примера — **да, `DeferAsync` лучше делать прямо в Integration handler-е до вызова MediatR**.

Почему:
- Discord interaction имеет жёсткий timeout на первичный ответ.
- `DeferAsync` — транспортный контракт Discord, не бизнес-логика.
- После `defer` ты спокойно выполняешь use-case и отправляешь follow-up/edit response.

### Рекомендуемый шаблон
```csharp
private async Task OnButtonExecuted(SocketMessageComponent messageComponent)
{
    try
    {
        // 1) ACK на границе
        await messageComponent.DeferAsync(ephemeral: true);

        // 2) Маппинг в твой application command
        var command = buttonMapper.Map(messageComponent);

        // 3) Вызов use-case
        var result = await mediator.Send(command);

        // 4) Отправка ответа через adapter/responder
        await discordResponder.RespondAsync(messageComponent, result);
    }
    catch (HttpException ex) when (ex.DiscordCode == DiscordErrorCode.InteractionHasAlreadyBeenAcknowledged)
    {
        logger.LogWarning(ex, "Interaction уже подтверждён ранее");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ошибка при обработке ButtonExecuted");
        // не throw, если не хочешь падать по event pipeline
    }
}
```

### Что важно исправить в твоём черновике
1. `mediator.Send();` — нужен конкретный command/запрос.
2. `OnButtonExecuted` должен быть `async Task` и реально `await`-ить операции.
3. Лог с `nameof(discordClientWrapper.DiscordSocketClient.ButtonExecuted)` ок, но лучше фиксированный event key (`"ButtonExecuted"`).
4. В event handler обычно не делают `throw` после логирования (иначе можно уронить/заспамить pipeline).

Итого: **да, defer делай в Integration до отправки твоего типа в MediatR**.

---

## Как переделать `DiscordEventsService` (до/после)

### Сейчас (как у тебя)
- `DiscordEventsService` публикует в MediatR события, содержащие `Socket*` типы.
- Это держит `Application` в связке с Discord SDK.

### Целевое состояние
1. В `Integrations.Discord` оставляешь подписки на `DiscordClient.*` события.
2. В каждом `On...` делаешь маппинг `Socket*` -> твой `Application` event/command DTO.
3. Публикуешь через MediatR **только свой тип** (без `Discord.*`).

Пример идеи:
```csharp
private async Task OnSlashCommandExecuted(SocketSlashCommand socketSlashCommand)
{
    var appCommand = slashMapper.Map(socketSlashCommand); // GetBotInfoCommand / ExecuteSlashCommand
    await mediator.Send(appCommand);
}
```

И для обычного события:
```csharp
private async Task OnMessageReceived(SocketMessage socketMessage)
{
    var appEvent = messageMapper.Map(socketMessage); // IncomingMessageEvent
    await mediator.Publish(appEvent);
}
```

### Что остаётся в Integrations
- `DeferAsync`, follow-up/edit response.
- Discord-specific проверки и lifecycle нюансы interaction.

### Что уходит в Application
- Бизнес-правила и use-case решения.
- Работа с портами (интерфейсами), а не с SDK.

Итог: **да, всё верно — в MediatR уходят уже твои типы после маппинга**.

---

## Host vs Integrations: кто за что отвечает

Твоя мысль верная:

### Host (верхний слой / composition root)
- Чтение конфигов/секретов (token, connection strings, api keys).
- Настройка DI-контейнера.
- Регистрация реализаций портов (`Application interface` -> `Integration implementation`).
- Запуск жизненного цикла приложения (hosted services, shutdown hooks).

### Integrations.Discord (адаптер)
- Подписки на события Discord (`Socket*` events).
- Работа с lifecycle interaction (`defer`, follow-up, edit response).
- Маппинг `Socket*` <-> `Application DTO`.
- Вызов MediatR/use-cases и обратный маппинг результата в Discord сообщения.

### Граница между ними
- Host **не** содержит бизнес-обработчиков Discord событий.
- Integrations **не** хранит бизнес-правила, только адаптацию и orchestration транспорта.

Это как раз «здоровая архитектура» для твоего кейса.

---

## Нужно ли делать `switch(commandName)` в event handler

Коротко: **да, это рабочий и правильный старт**.

### Когда `switch` ок
- Команд немного (3-7).
- Логика маршрутизации простая.
- Ты быстро итерируешься и хочешь минимальную сложность.

### Когда пора выносить в роутер
- Команд становится много.
- Появляются разные политики/permissions/валидация на этапе маршрутизации.
- Handler начинает разрастаться и становится сложно тестировать.

### Рекомендуемая эволюция
1. Сначала:
```csharp
switch (commandName)
{
    case "bot-info":
        await mediator.Send(new GetBotInfoCommand(...));
        break;
    case "set-voiceroom":
        await mediator.Send(new SetupVoiceRoomCommand(...));
        break;
}
```
2. Потом (когда вырастет): `IDiscordCommandRouter` + словарь `commandName -> mapper + mediator.Send`.

### Важно
- `switch` живёт в **Integrations**, не в Application.
- В `mediator.Send(...)` отправляешь только свои команды/DTO.
- Discord типы (`Socket*`) остаются на границе.

Итог: да, твой подход сейчас правильный; просто держи в голове следующий шаг — router/dispatcher при росте количества команд.

---

## Что выбрать «лучше всего» прямо сейчас (рекомендуемый baseline)

Если нужен один практичный ответ для твоего текущего этапа, бери этот вариант:

1. `DiscordEventBinder` делает только подписку/отписку и прокидывает событие в **тонкий** handler.
2. Тонкий integration handler:
   - сразу делает `DeferAsync`,
   - вызывает `IDiscordCommandRouter.RouteAsync(...)`.
3. `IDiscordCommandRouter` внутри `switch`/таблицы маршрутов маппит в `Application` command и вызывает `mediator.Send(...)`.
4. `Application` возвращает `Result/DTO`, adapter формирует Discord response.

Почему это лучший компромисс:
- достаточно просто внедрить сейчас;
- не засоряет `Application` Discord-типами;
- масштабируется до router/strategy без переписывания ядра.

### Чего я бы не делал на старте
- Не плодил бы промежуточные integration notifications через `mediator.Publish`, если они не дают явной пользы.
- Не выносил бы сразу всё в сложные паттерны, пока команд немного.

Итого: **тонкий integration handler + `IDiscordCommandRouter` + application commands через MediatR**.

---

## `OnSlashCommandExecuted`: хэндлить сразу или через `mediator.Publish`?

Оба подхода валидны:

### Вариант A (проще): хэндлить сразу в binder-е
- `DeferAsync`
- `router.RouteAsync(...)`
- fallback/логирование

Плюс: меньше слоёв и абстракций.
Минус: binder может стать «толстым».

### Вариант B (чистое разделение): `mediator.Publish` -> integration handler
- Binder только «поднимает» событие.
- Вся обработка в `SlashCommandExecutedHandler`.

Плюс: registration и обработка разделены, легче тестировать отдельно.
Минус: чуть больше инфраструктурного кода.

### Что выбрать тебе сейчас
- Если команд и событий пока немного — **вариант A** (быстрее).
- Если хочешь единый паттерн на все Discord events — **вариант B**.

### Важное независимо от варианта
1. Не забудь `DeferAsync` на границе interaction lifecycle.
2. Не логируй `ButtonExecuted` в `OnSlashCommandExecuted` (event name должен совпадать).
3. В `Application` не протаскивай `SocketSlashCommand`.

---

## Твой текущий вариант с `SlashCommandExecutedNotification`: куда ставить dispatcher

Да, в твоей схеме dispatcher (`IDiscordCommandRouter`) вызывается **внутри integration handler-а**.

### Правильный flow для твоей структуры
1. `DiscordEventBinder` ловит `SocketSlashCommand`.
2. Паблишит integration notification (`SlashCommandExecutedNotification`) — это ещё нормально, если хочешь разделить registration и обработку.
3. `SlashCommandExecutedHandler`:
   - делает `DeferAsync` (или проверяет, что interaction уже ACK),
   - вызывает `IDiscordCommandRouter.RouteAsync(notification.SocketSlashCommand, ct)`,
   - обрабатывает `IsHandled == false` (unknown command).

### Мини-скелет
```csharp
public class SlashCommandExecutedHandler(
    IDiscordCommandRouter router,
    ILogger<SlashCommandExecutedHandler> logger) : INotificationHandler<SlashCommandExecutedNotification>
{
    public async Task Handle(SlashCommandExecutedNotification notification, CancellationToken ct)
    {
        try
        {
            await notification.SocketSlashCommand.DeferAsync(ephemeral: true);

            var route = await router.RouteAsync(notification.SocketSlashCommand, ct);

            if (!route.IsHandled)
                await notification.SocketSlashCommand.FollowupAsync("Команда не поддерживается", ephemeral: true);
        }
        catch (HttpException ex) when (ex.DiscordCode == DiscordErrorCode.InteractionHasAlreadyBeenAcknowledged)
        {
            logger.LogWarning(ex, "Interaction уже подтверждён");
        }
    }
}
```

### Что поправить в твоём коде прямо сейчас
1. В `LogError` у тебя текст про `ButtonExecuted`, хотя обрабатываешь `SlashCommandExecuted`.
2. В `Bind()` желательно добавить `Unbind()` (симметричная отписка), чтобы не ловить дубли подписок.
3. Если integration notification не даёт ценности — можно упростить: Binder -> Router напрямую (без `mediator.Publish`).

Итог: **да, dispatcher вызывай в integration handler-е (или напрямую из binder-а, если хочешь проще).**

---

## Что такое `IDiscordCommandRouter` и как он выглядит

`IDiscordCommandRouter` — это adapter-компонент, который:
1. Принимает Discord interaction (`SocketSlashCommand` или уже mapped context).
2. Определяет, какой Application command нужно создать по `commandName`.
3. Делегирует выполнение через `IMediator`.

То есть это «диспетчер маршрутов», чтобы не держать огромный `switch` в event binder-е.

### Интерфейс
```csharp
public interface IDiscordCommandRouter
{
    Task<RouteResult> RouteAsync(SocketSlashCommand slashCommand, CancellationToken ct = default);
}

public sealed record RouteResult(bool IsHandled, string? Error = null);
```

### Пример реализации (минимальный)
```csharp
public sealed class DiscordCommandRouter(
    IMediator mediator,
    IDiscordSlashMapper mapper) : IDiscordCommandRouter
{
    public async Task<RouteResult> RouteAsync(SocketSlashCommand slashCommand, CancellationToken ct = default)
    {
        switch (slashCommand.CommandName)
        {
            case "bot-info":
            {
                var cmd = mapper.MapBotInfo(slashCommand);
                await mediator.Send(cmd, ct);
                return new RouteResult(true);
            }
            case "set-voiceroom":
            {
                var cmd = mapper.MapSetupVoiceRoom(slashCommand);
                await mediator.Send(cmd, ct);
                return new RouteResult(true);
            }
            default:
                return new RouteResult(false, $"Unknown command: {slashCommand.CommandName}");
        }
    }
}
```

### Event handler тогда становится тонким
```csharp
private async Task OnSlashCommandExecuted(SocketSlashCommand slashCommand)
{
    await slashCommand.DeferAsync(ephemeral: true);

    var route = await router.RouteAsync(slashCommand);

    if (!route.IsHandled)
        await slashCommand.FollowupAsync($"Команда не поддерживается: {slashCommand.CommandName}", ephemeral: true);
}
```

### DI регистрация
```csharp
services.AddScoped<IDiscordCommandRouter, DiscordCommandRouter>();
services.AddScoped<IDiscordSlashMapper, DiscordSlashMapper>();
```

### Как эволюционировать дальше
Если `switch` разросся, переходишь на словарь стратегий:
- `Dictionary<string, IDiscordCommandHandler>`
- где каждый handler маппит и вызывает свой `mediator.Send`.

---

## Сценарий: сервисная slash-команда `/bot-info`

Да, твоя последовательность правильная. Эталонный flow такой:

1. **Presentation/Integrations.Discord**
   - Регистрируешь slash-команду `/bot-info` в Discord.
   - Ловишь `SocketSlashCommand` событие.

2. **Integrations (adapter handler)**
   - Делаешь `DeferAsync` (обычно здесь, на границе).
   - Маппишь `SocketSlashCommand` -> `GetBotInfoCommand` (твой DTO/command).
   - Передаёшь команду в MediatR (`Application`).

3. **Application**
   - Handler работает только с твоими типами и портами.
   - Собирает данные (uptime, версия, статус БД, количество гильдий и т.д.) через интерфейсы.
   - Возвращает `BotInfoResult` (или `Result<BotInfoDto>`).

4. **Integrations (adapter response)**
   - Маппишь `BotInfoResult` -> Discord embed/message.
   - Отправляешь follow-up/edit response в Discord.

### Где делать `defer`?
- В большинстве случаев — **в Integrations/Presentation**, сразу после получения события.
- Почему: это транспортная обязанность (тайминги interaction lifecycle Discord), не бизнес-логика.

### Какие поля передавать в Application
Минимум:
- `RequestId/TraceId`
- `GuildId`
- `ChannelId`
- `MemberId`
- `CommandName`
- `Locale` (если нужна локализация)

### Чего не делать
- Не тащить `SocketSlashCommand` в `Application`.
- Не формировать Discord embed внутри use-case.
- Не делать `DeferAsync/RespondAsync` внутри Application handler.

Итог: **да, ты описал правильный pipeline**. Единственное уточнение — defer/response лучше оставить в adapter-слое.

---

## Да, ты правильно понял (целевая схема handler-а)

Твой `Application` handler должен:
1. Принимать **твой command/query DTO** (а не `Socket*`).
2. Вызывать интерфейсы (порты), **объявленные в Application**.
3. Не импортировать `Discord.*`.
4. Возвращать результат use-case (`Result`, `Error`, `Output DTO`).

А `Presentation/Adapters` должны:
1. Принять событие/команду Discord (`Socket*`).
2. Смаппить в твой `Application DTO`.
3. Вызвать MediatR/use-case.
4. Смаппить результат обратно в Discord response.

Это и есть правильная порт-адаптер схема для твоего кейса.

---

## Где хранить DTO для MediatR (практичная структура)

Лучшее место: **`Application` слой**, рядом с use-case, который их использует.

### Рекомендованный вариант (feature-first)
```text
Amnyam.Application/
  Features/
    BotInfo/
      GetBotInfoCommand.cs        // IRequest<GetBotInfoResult>
      GetBotInfoHandler.cs
      GetBotInfoResult.cs
      GetBotInfoValidator.cs       // если используешь FluentValidation
```

### Альтернатива (по типам)
```text
Amnyam.Application/
  Contracts/
    Commands/
    Queries/
    Results/
```

### Что выбрать
- Если проект растёт и много вертикальных сценариев — **feature-first** (предпочтительно).
- Если маленький проект и мало use-cases — можно `Contracts/*`.

### Важные правила
1. DTO для MediatR (`IRequest`, `INotification`, result DTO) — в `Application`.
2. DTO транспорта (`Socket*` wrappers, webhook payload) — в `Integrations`.
3. Domain-сущности не смешивать с DTO запроса/ответа.

---

## Где хранить DTO по слоям (ответ на твой вопрос)

### Коротко
- **Application DTO (use-case контракты)** — в `Amnyam.Application` (часто в папке `Contracts`/`Dtos`).
- **Domain** — только сущности, value objects, доменные правила/инварианты (обычно без DTO).
- **Infrastructure/Integrations DTO** — отдельные transport/integration модели рядом с адаптером (например, Discord/OpenAI specific).

### Если у тебя есть проект `Core`
- Если `Core` = `Domain + Application`, тогда DTO допустимы в `Core.Application` части.
- Если `Core` = чистый Domain, **не клади туда application DTO**.

### Практичное правило
DTO лежит в том слое, где живёт контракт:
1. Контракт use-case (`Command/Query/Input/Output`) → `Application`.
2. Контракт внешнего API/SDK (`Socket*`, webhook payload и т.п.) → `Integrations/Adapters`.
3. Контракт persistence (`EF projection`, `read model`) → `Infrastructure`.

Это убирает «утечки» и помогает не перепутать бизнес-модели с транспортными.

---

## Где можно оставить платформенную связанность (и это нормально)
- Layer adapters (`Integrations.Discord`).
- Формат embed/components.
- Нюансы interaction lifecycle (`defer`, `ephemeral`, retries).

Это **здоровая связанность**, потому что она изолирована.

---

## Критерий, что архитектура «достаточно чистая»
Спроси себя:
1. Могу ли я протестировать ключевую бизнес-логику без Discord клиента?  
2. Если Discord SDK обновится, меняю ли я в основном adapter-слой, а не domain/use-case?  
3. Могу ли я переиспользовать use-cases в другом интерфейсе (например, web admin panel)?

Если ответы “да” — архитектура у тебя в правильном состоянии.

---

## TL;DR
- Ты двигаешься правильно.
- Для Discord-бота не нужна «идеально академическая» чистая архитектура.
- Нужна **прагматичная**: чистое ядро + изолированный платформенный adapter.
- Твой следующий уровень как инженера — научиться системно держать эти границы и покрыть use-cases тестами.
