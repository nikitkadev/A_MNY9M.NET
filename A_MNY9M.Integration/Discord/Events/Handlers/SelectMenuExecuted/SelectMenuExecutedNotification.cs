using MediatR;

using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Events.Handlers.SelectMenuExecuted;

public class SelectMenuExecutedNotification(SocketMessageComponent component) : INotification
{
    public SocketMessageComponent Component { get; set; } = component;
}
