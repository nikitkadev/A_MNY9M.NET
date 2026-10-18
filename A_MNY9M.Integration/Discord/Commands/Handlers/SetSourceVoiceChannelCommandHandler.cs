using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Core.Common.Enums;
using A_MNY9M.Core.Common.Result;

using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

using A_MNY9M.Integration.Services;
using A_MNY9M.Integration.Models.Params;

namespace A_MNY9M.Integration.Discord.Commands.Handlers;

public sealed class SetSourceVoiceChannelCommandHandler(
    ILogger<SetSourceVoiceChannelCommandHandler> logger,
    GuildLogService guildLogService,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task<Result<Guid>> HandleAsync(SetSourceVoiceChannelParams request)
    {
        try
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var repository = scope.ServiceProvider
                .GetRequiredService<SourceVoiceChannelRepository>();

            var result = await repository.AddSourceVoiceChannelAsync(
                new SourceVoiceChannelDbEntity()
                {
                    DiscordId = request.DiscordId,
                    GuildCategoryId = request.DiscordCategoryId,
                    InitiatorDiscordId = request.InitiatorDiscordId,
                    Name = request.Name,
                    SetAt = DateTime.UtcNow
                },
                cancellationToken: default);

            if (result.IsSuccess)
            {
                await guildLogService.TrySendLogAsync(
                    $"Канал с id {result.Value} успешно назначен источником.");
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Не удалось назначить канал {ChannelId} источником. Инициатор: {InitiatorId}.",
                request.DiscordId,
                request.InitiatorDiscordId);

            return Result<Guid>.Fail(
                "Произошла ошибка при попытке назначить канал. Повторите попытку позже.",
                new CustomError(
                    Type: CustomErrorType.Internal,
                    Code: "source_voice_channel.save_failed",
                    ex.Message));
        }


    }
}
