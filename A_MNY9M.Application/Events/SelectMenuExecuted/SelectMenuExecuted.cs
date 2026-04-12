using MediatR;

using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.SelectMenuExecuted;

class SelectMenuExecuted(SocketMessageComponent socketMessageComponent) : INotification
{
    public SocketMessageComponent SocketMessageComponent { get; set; } = socketMessageComponent;
}
