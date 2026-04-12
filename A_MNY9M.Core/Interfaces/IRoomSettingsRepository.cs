using A_MNY9M.Core.Entities.Configurations;

namespace A_MNY9M._1_Core.Interfaces;

public interface IRoomSettingsRepository
{
    Task<VoiceChannelConfiguration> GetRoomSettingsByGuildMemberDiscordIdAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task RemoveRoomSettingsByGuildMemberDiscordIdAsync(ulong guildMemberDiscordId, CancellationToken token = default);
    Task UpsertRoomSettingsAsync(VoiceChannelConfiguration roomSettings, CancellationToken token = default);
}
