using Discord.WebSocket;
using MediatR;

namespace A_MNY9M.Integration.Discord.Events.Handlers.UserJoined;

public class UserJoinedNotification(SocketGuildUser user) : INotification
{
    public SocketGuildUser SocketGuildUser { get; set; } = user;
}
