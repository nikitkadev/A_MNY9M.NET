namespace A_MNY9M.Core.Entities.Guild.Channel.Base;

public class BaseGuildChannel
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }
    public ulong GuildId { get; set; }
    public ulong CategoryId { get; set; }
    public string ChannelName { get; set; } = string.Empty;
}
