using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Core.Common.Result;
using A_MNY9M.Integration.Services;
using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Infrastructure.Database.Entities.Settings;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

namespace A_MNY9M.Integration.Discord.Commands.Handlers;

public sealed class SetAutoLobbySettingsCommandHandler(
    GuildLogService guildLogService,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task<Result<Guid>> HandleAsync(
        ulong userDiscordId,
        string userMention,
        AutoVoiceChannelSettingsDto settingsDto)
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

        await guildLogService.SendLogAsync(
            $"Пользователь {userMention} успешно сохранил настройки личной комнаты.");

        return result;
    }
}
