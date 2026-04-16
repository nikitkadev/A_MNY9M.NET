using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M.Core.Interfaces.Repository;

public interface IGuildVoiceSessionRepository
{
    public Task AddGuildVoiceSessionAsync(VoiceSession session);
}
