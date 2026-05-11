using MediatR;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.ButtonExecuted;

public class ButtonExecutedHandler(
    IDiscordV2ComponentsBuilder discordV2ComponentsBuilder) : INotificationHandler<ButtonExecutedNotification>
{
    public async Task Handle(
        ButtonExecutedNotification notification, 
        CancellationToken cancellationToken)
    {
        switch (notification.Component.Data.CustomId)
        {
            case ButtonsCustomIdConsts.Rules:

                await notification.Component.DeferAsync();
                var rulesComponent = await discordV2ComponentsBuilder.BuildRulesMessageComponentAsync();

                await notification.Component.FollowupAsync(
                    components: rulesComponent,
                    ephemeral: true);

                break;

            default:
                await notification.Component.DeferAsync();
                await notification.Component.FollowupAsync(
                    "Пук",
                    ephemeral: true);

                break;
        }
    }
}
