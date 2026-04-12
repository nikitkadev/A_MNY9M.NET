using MediatR;

using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.MessageReceived;

public class MessageReceived(SocketMessage socketMessage) : INotification
{
    public SocketMessage SocketMessage { get; set; } = socketMessage;
}
