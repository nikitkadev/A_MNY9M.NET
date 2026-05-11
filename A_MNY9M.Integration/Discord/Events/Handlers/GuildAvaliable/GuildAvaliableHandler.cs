using MediatR;

using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.GuildAvaliable;

public class GuildAvaliableHandler(
    IDiscordAnchorMessageUpdater anchorMessageUpdater) : INotificationHandler<GuildAvaliableNotification>
{
    public async Task Handle(
        GuildAvaliableNotification notification, 
        CancellationToken cancellationToken)
    {
        await anchorMessageUpdater.UpdateAsync();
    }
}
