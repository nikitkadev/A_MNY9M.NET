using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordSlashCommandRouter
{
    Task RouteAsync(SocketSlashCommand command, CancellationToken cancellationToken = default);
}
