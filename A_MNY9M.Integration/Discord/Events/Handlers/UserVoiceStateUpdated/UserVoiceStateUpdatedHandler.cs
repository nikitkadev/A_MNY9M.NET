using Microsoft.Extensions.Options;

using MediatR;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.UserVoiceStateUpdated;

public class UserVoiceStateUpdatedHandler(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordChannelsService discordChannelsService) : INotificationHandler<UserVoiceStateUpdatedNotification>
{
    public async Task Handle(
        UserVoiceStateUpdatedNotification notification, 
        CancellationToken cancellationToken)
    {
        if(notification.OldState.VoiceChannel is not null)
        {
            var isTemporaryVoiceChannel = discordChannelsService.IsTemporaryVoiceChannel(notification.OldState.VoiceChannel.Id);

            if (isTemporaryVoiceChannel && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
            {
                await discordChannelsService.ClearTemporaryVoiceChannelAsync(
                    channelId: notification.OldState.VoiceChannel.Id);
            }
        }

        if(notification.NewState.VoiceChannel is not null)
        {
            var spawners = malenkieOptions.Value.Channels.Voice.SessionSpawners.Select(x => x.Value);

            if (spawners.Contains(notification.NewState.VoiceChannel.Id))
            {
                await discordChannelsService.CreateTemporaryVoiceChannelAsync(
                    leader: (SocketGuildUser)notification.User,
                    categoryId: notification.NewState.VoiceChannel.CategoryId);
            }
        }
    }
}