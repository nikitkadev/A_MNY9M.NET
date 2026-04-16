using A_MNY9M.Core.Entities.Guild.Member;

namespace A_MNY9M.Core.Interfaces.Repository;

public interface IGuildMembersRepository
{
    Task UpsertGuildMemberAsync(GuildMember guildMember, CancellationToken token = default);
    Task <GuildMember> GetGuildMemberEntityAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task RemoveGuildMemberEntityFromDbAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task SyncGuildMembersWithDbAsync(CancellationToken token = default);

    Task<bool> IsAuthorizedAsync(ulong guildMemberDiscordId, CancellationToken token = default);
}
