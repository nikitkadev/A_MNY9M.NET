using Discord.WebSocket;
using MediatR;

namespace A_MNY9M.Integration.Discord.Events.Handlers.ButtonExecuted;

public class ButtonExecutedNotification(SocketMessageComponent component) : INotification
{
    public SocketMessageComponent Component { get; set; } = component;
}
