using Discord;

namespace A_MNY9M.Integration.Discord.Components.V2;

public static class RulesV2ComponentBuilder
{
    public static MessageComponent Build(IEmote dotEmote)
    {
        var messageComponent = new ComponentBuilderV2()
            .WithContainer(container =>
            {
                container.WithTextDisplay("### Правила сервера ᴍᴀʟᴇɴᴋɪᴇ");
                container.WithTextDisplay(
                    $"{dotEmote} Правило 1: Кто не бык, тот буйвол. Помни эту простую истину ;\n" +
                    $"{dotEmote} Правило 2: Ронин лысый. Это не оскорбление, а биологический факт ;\n" +
                    $"{dotEmote} Правило 3: Не быкуй, если не знаешь алфавита ;\n" +
                    $"{dotEmote} Правило 4: Не скидывай чунга-чангу. Никогда. Даже если очень хочется ;\n" +
                    $"{dotEmote} Правило 5: Остерегайтесь мифических существ (мод-самолет, админ циса, гузман) ;\n" +
                    $"{dotEmote} Правило 6: Не пытайтесь убежать от казахов - у вас не получится ;\n" +
                    $"{dotEmote} Правило 7: Закрывайте глаза, когда Леха кидает гифки ;\n" +
                    $"{dotEmote} Правило 8: Жизнь говно, а мы в ней дно. Не верьте этому. ;");
            })
            .Build();

        return messageComponent;
    }
}