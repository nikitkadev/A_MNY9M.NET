using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M._1_Core.Interfaces;

public interface IGuildVoiceSessionRepository
{
    public Task AddGuildVoiceSessionAsync(VoiceSession session);
}
