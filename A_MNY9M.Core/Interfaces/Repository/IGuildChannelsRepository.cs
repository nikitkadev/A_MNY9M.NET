using A_MNY9M.Core.Entities.Guild.Channel.Text;
using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M.Core.Interfaces.Repository;

public interface IGuildChannelsRepository
{
    Task SyncDbVoiceChannelsWithGuildAsync(CancellationToken token = default);

    Task UpsertDbTextChannelAsync(GuildTextChannel channel);
    Task UpsertDbVoiceChannelAsync(GuildVoiceChannel channel);

    Task RemoveDbTextChannelAsync(ulong id);
    Task RemoveDbVoiceChannelAsync(ulong id);

    Task<bool> IsTemporaryVoiceChannel(ulong id, CancellationToken token = default);
    Task<bool> IsGeneratingVoiceChannel(ulong id, CancellationToken token = default);
}
