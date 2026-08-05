using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordRolesManager
{
    Task UploadUserSelectedCategoryRolesAsync(SocketGuildUser user, IReadOnlyCollection<string> values);
    Task UploadUserSelectedNameColorAsync(SocketGuildUser user, IReadOnlyCollection<string> values);
    Task SetRolesToUserAsync(ulong userId, List<ulong> roleIds);
}
