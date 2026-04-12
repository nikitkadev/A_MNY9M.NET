using MediatR;

using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.GuildAvailable;

class GuildAvailable(SocketGuild socketGuild) : INotification
{
    public SocketGuild SocketGuild { get; } = socketGuild;
}
