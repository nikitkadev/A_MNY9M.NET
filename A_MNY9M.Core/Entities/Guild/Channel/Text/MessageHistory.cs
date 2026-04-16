using A_MNY9M.Core.Common;

namespace A_MNY9M.Core.Entities.Guild.Channel.Text;

public class MessageHistory
{
    public int Id { get; set; }

    public ulong MessageDiscordId { get; set; }
    public ulong ChannelDiscordId { get; set; }
    public ulong SenderDiscordId { get; set; }

    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;

    public string? Content { get; set; }
    public SentMessageType Types { get; set; } = SentMessageType.None;
}