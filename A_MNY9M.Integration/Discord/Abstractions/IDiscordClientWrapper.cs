using Discord;
using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordClientWrapper
{
    DiscordSocketClient DiscordSocketClient { get; }
    SocketGuild MlkGuild { get; }
    Task<Emote> GetApplicationEmoteAsync(ulong discordId);
}
