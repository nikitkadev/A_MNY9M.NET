using MediatR;

using A_MNY9M.Core.Interfaces.Services;

namespace A_MNY9M.Integration.Discord.Events.Handlers.UserJoined;

public class UserJoinedHandler(
    IUserService userService) : INotificationHandler<UserJoinedNotification>
{
    public async Task Handle(
        UserJoinedNotification notification, 
        CancellationToken cancellationToken)
    {
        await userService.ExecuteJoinPipelineAsync(
            userId: notification.SocketGuildUser.Id,
            userMention: notification.SocketGuildUser.Mention);
    }
}
