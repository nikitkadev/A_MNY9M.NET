using MediatR;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.SelectMenuExecuted;

public class SelectMenuExecutedHandler(
    IDiscordRolesManager discordRolesManager) : INotificationHandler<SelectMenuExecutedNotification>
{
    public async Task Handle(
        SelectMenuExecutedNotification notification, 
        CancellationToken cancellationToken)
    {
        switch (notification.Component.Data.CustomId)
        {
            case SelectionMenuIdConsts.RolesMenu:

                await notification.Component.DeferAsync();

                await discordRolesManager.UploadUserRolesAsync(
                    user: (SocketGuildUser)notification.Component.User,
                    values: notification.Component.Data.Values);

                await notification.Component.FollowupAsync(
                    text: "Роли успешно обновлены",
                    ephemeral: true);

                break;
        }
    }
}
