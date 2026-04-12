using MediatR;
using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.ButtonExecuted;

public class ButtonExecuted(SocketMessageComponent socketMessageComponent) : INotification
{
    public SocketMessageComponent SocketMessageComponent { get; } = socketMessageComponent;
}
