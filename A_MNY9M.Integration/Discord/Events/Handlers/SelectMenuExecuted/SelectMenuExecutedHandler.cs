using MediatR;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.SelectMenuExecuted;

public class SelectMenuExecutedHandler(
    IDiscordV2ComponentsBuilder discordV2ComponentsBuilder,
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

                await discordRolesManager.UploadUserSelectedCategoryRolesAsync(
                    user: (SocketGuildUser)notification.Component.User,
                    values: notification.Component.Data.Values);

                await notification.Component.FollowupAsync(
                    components: discordV2ComponentsBuilder.BuildDefaultMessageComponent(
                        text: $"Роли обновлены успешно {MarkdownEmote.CoolDogie}"),
                    ephemeral: true);

                break;

            case SelectionMenuIdConsts.ColorsMenu:

                await notification.Component.DeferAsync();

                await discordRolesManager.UploadUserSelectedNameColorAsync(
                    user: (SocketGuildUser)notification.Component.User,
                    values: notification.Component.Data.Values);

                await notification.Component.FollowupAsync(
                    components: discordV2ComponentsBuilder.BuildDefaultMessageComponent(
                        text: $"Роли для цвета имени успешно обновлены {MarkdownEmote.CoolDogie}"),
                    ephemeral: true);

                break;
        }
    }
}
