using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordRolesManager
{
    Task UploadUserRolesAsync(SocketGuildUser user, IReadOnlyCollection<string> values);
}
