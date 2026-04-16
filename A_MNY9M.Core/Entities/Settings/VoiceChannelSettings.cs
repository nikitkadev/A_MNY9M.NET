using A_MNY9M.Core.Common;

namespace A_MNY9M.Core.Entities.Settings;

public class VoiceChannelSettings
{
    public int Id { get; set; }

    public ulong MemberDiscordId { get; set; }
    public string VoiceChannelName { get; set; } = ServiceConsts.VoiceChannelDefaultName;
    public string? Region { get; set; }
}
