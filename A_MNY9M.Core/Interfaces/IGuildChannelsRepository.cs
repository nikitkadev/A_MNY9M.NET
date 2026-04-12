using A_MNY9M._1_Core.Entities;
using A_MNY9M.Core.Entities.Guild;

namespace A_MNY9M._1_Core.Interfaces;

public interface IGuildChannelsRepository
{
    Task SyncDbVoiceChannelsWithGuildAsync(CancellationToken token = default);

    Task UpsertDbTextChannelAsync(GuildChannel channel);
    Task UpsertDbVoiceChannelAsync(GuildVoiceChannel channel);

    Task RemoveDbTextChannelAsync(ulong id);
    Task RemoveDbVoiceChannelAsync(ulong id);

    Task<bool> IsTemporaryVoiceChannel(ulong id, CancellationToken token = default);
    Task<bool> IsGeneratingVoiceChannel(ulong id, CancellationToken token = default);
}
