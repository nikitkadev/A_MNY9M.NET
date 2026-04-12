using Discord;
using Discord.WebSocket;

namespace A_MNY9M._3_Infrastructure.Interfaces.Discord;

public interface IDiscordService
{
    DiscordSocketClient DiscordClient { get; }
    Task<SocketGuildUser> GetGuildMemberAsync(ulong guildMemberDiscordId);
    Task<string> GetGuildMemberMentionByIdAsync(ulong memberId);
    SocketGuild GetSocketGuild();
    GuildEmote? GetGuildEmote(string emoteKey);
}
