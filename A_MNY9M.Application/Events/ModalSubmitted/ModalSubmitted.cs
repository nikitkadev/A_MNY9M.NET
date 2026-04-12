using Discord.WebSocket;

using MediatR;

namespace A_MNY9M._2_Application.Events.ModalSubmitted;

class ModalSubmitted(SocketModal modal) : INotification
{
    public SocketModal Modal { get; } = modal;
}
