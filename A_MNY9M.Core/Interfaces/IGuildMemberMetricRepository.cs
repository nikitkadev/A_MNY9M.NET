using A_MNY9M.Core.Entities;

namespace A_MNY9M._1_Core.Interfaces;

public interface IGuildMemberMetricRepository
{
    Task IncrementMessageSentCountAsync(ulong guildMemberDiscordId, int increment = 1);
    Task IncrementReactionAddedCountAsync(ulong guildMemberDiscordId, int increment = 1);
    Task IncrementCommandSentCountAsync(ulong guildMemberDiscordId, int increment = 1);
    Task IncrementStickerCountAsync(ulong guildMemberDiscordId, int increment = 1);
    Task IncrementGifSentCountAsync(ulong guildMemberDiscordId, int increment = 1);
    Task IncrementPngPictureSentCountAsync(ulong guildMemberDiscordId, int increment = 1);

    Task UpdateLastMessageDateAsync(ulong guildMemberDiscordId);

    Task<MemberMetric> GetGuildMemberMetricAsync(ulong guildMemberDiscordId);
}
