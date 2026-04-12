using A_MNY9M._1_Core.Constants;

namespace A_MNY9M.Core.Entities.Configurations;

public class VoiceChannelConfiguration
{
    public int Id { get; set; }

    public ulong MemberDiscordId { get; set; }
    public string VoiceChannelName { get; set; } = A_MNY9M_CONSTS.DEFAULT_VOICEROOM_NAME;
    public string? Region { get; set; }
}
