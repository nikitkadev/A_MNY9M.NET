using Discord.WebSocket;
using MediatR;

namespace A_MNY9M.Integration.Discord.Events.Handlers.GuildAvaliable;

public class GuildAvaliableNotification(SocketGuild socketGuild) : INotification
{
    public SocketGuild SocketGuild { get; set; } = socketGuild;
}