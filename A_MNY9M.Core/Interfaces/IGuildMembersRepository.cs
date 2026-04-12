using A_MNY9M._1_Core.Entities;

namespace A_MNY9M._1_Core.Interfaces;

public interface IGuildMembersRepository
{
    Task UpsertGuildMemberAsync(GuildMember guildMember, CancellationToken token = default);
    Task <GuildMember> GetGuildMemberEntityAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task RemoveGuildMemberEntityFromDbAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task SyncGuildMembersWithDbAsync(CancellationToken token = default);
    Task<bool> IsAuthorizedAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task<long> GetTotalSecondsInVoiceChannelsByMemberDiscordIdAsync(ulong guildMemberDiscordId);
}
