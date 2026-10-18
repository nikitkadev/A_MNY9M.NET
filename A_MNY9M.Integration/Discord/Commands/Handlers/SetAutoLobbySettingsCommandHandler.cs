using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Core.Common.Enums;
using A_MNY9M.Core.Common.Result;

using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Infrastructure.Database.Entities.Settings;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

using A_MNY9M.Integration.Services;

namespace A_MNY9M.Integration.Discord.Commands.Handlers;

public sealed class SetAutoLobbySettingsCommandHandler(
    ILogger<SetAutoLobbySettingsCommandHandler> logger,
    GuildLogService guildLogService,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task<Result<Guid>> HandleAsync(
        ulong userDiscordId,
        string userMention,
        AutoVoiceChannelSettingsDto settingsDto)
    {
        try
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var repository = scope.ServiceProvider.GetRequiredService<AutoVoiceChannelRepository>();

            var result = await repository.SaveAutoVoiceChannelSettingsAsync(
                new AutoVoiceChannelSettingsDbEntity()
                {
                    DefaultName = settingsDto.Name,
                    Region = settingsDto.Region,
                    UserDiscordId = userDiscordId,
                    UserLimit = settingsDto.UserLimit,
                });

            if (result.IsSuccess)
            {
                await guildLogService.TrySendLogAsync(
                    $"Пользователь {userMention} успешно сохранил настройки личной комнаты.");
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Произошла ошибка при попытке сохранить настройки личной комнаты пользователя {UserId}.",
                userDiscordId);

            return Result<Guid>.Fail(
                "Произошла ошибка при попытке настроить личную комнату. Повторите попытку позже.",
                new CustomError(
                    Type: CustomErrorType.Internal,
                    Code: "auto_voice_channel_settings.failed_saving",
                    ex.Message));
        }

    }
}
