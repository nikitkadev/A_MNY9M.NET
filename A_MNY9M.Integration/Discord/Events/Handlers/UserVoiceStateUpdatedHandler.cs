using Microsoft.Extensions.DependencyInjection;

using Discord.WebSocket;

using A_MNY9M.Integration.Services;
using A_MNY9M.Infrastructure.Repositories;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class UserVoiceStateUpdatedHandler(
    GuildLogService logService,
    IServiceScopeFactory scopeFactory,
    DiscordChannelService discordChannelService)
{
    public async Task HandleAsync(
        SocketUser user,
        SocketVoiceState oldState,
        SocketVoiceState newState)
    {
        await using var scope = scopeFactory.CreateAsyncScope();

        var discordChannelRepository = scope.ServiceProvider.GetRequiredService<DiscordChannelRepository>();

        var oldChannel = oldState.VoiceChannel;
        var newChannel = newState.VoiceChannel;

        if (oldChannel is not null
            && oldChannel.ConnectedUsers.Count == 0
            && oldChannel.Id != newChannel?.Id)
        {
            var isAutoChannel = await discordChannelRepository.IsAutoVoiceChannelAsync(oldState.VoiceChannel.Id);

            if (isAutoChannel)
            {
                await oldState.VoiceChannel.DeleteAsync();
                await discordChannelRepository.RemoveAutoVoiceChannelAsync(oldChannel.Id);

                await logService.SendLogAsync(
                    $"Канал {oldState.VoiceChannel.Name} " +
                    $"с DiscordId {oldState.VoiceChannel.Id} " +
                    $"был автоматически удален.");
            }
        }

        if (newChannel is not null
           && newChannel.Id != oldChannel?.Id)
        {
            var isSourceChannel = await discordChannelRepository.IsSourceVoiceChannelAsync(newChannel.Id);

            if (isSourceChannel)
            {
                var createdChannel = await discordChannelService.CreateAutoVoiceChannelAsync(user.Id);

                if (createdChannel is not null && user is SocketGuildUser guildUser)
                {
                    await guildUser.ModifyAsync(option =>
                    {
                        option.ChannelId = createdChannel.Id;
                    });

                    await logService.SendLogAsync(
                        $"Пользователь {user.Mention} успешно создал авто-канал и был перемещен в него.");
                }
            }
        }
    }
}