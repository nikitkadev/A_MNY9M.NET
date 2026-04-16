using A_MNY9M.Core.Entities.Guild.Channel.Base;

namespace A_MNY9M.Core.Entities.Guild.Channel.Voice;

public class GuildVoiceChannel : BaseGuildChannel
{
    public bool IsGeneratingChannel { get; set; }
    public bool IsTemporaryChannel { get; set; }
}
