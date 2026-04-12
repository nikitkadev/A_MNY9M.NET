using Discord.WebSocket;
using MediatR;

namespace A_MNY9M._2_Application.Events.UserJoined
{
    class UserJoined(SocketGuildUser socketGuildUser) : INotification
    {
        public SocketGuildUser SocketGuildUser { get; } = socketGuildUser;
    }
}
