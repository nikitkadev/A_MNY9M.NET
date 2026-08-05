namespace A_MNY9M.Core.Entities.Metrics;

public class MemberMessageMetric
{
    public int Id { get; set; }

    public ulong GuildId { get; set; }
    public ulong MemberDiscordId { get; set; }

    public int MessageCount { get; set; }
    public int ReactionAddedCount { get; set; }
    public int CommandsSentCount { get; set; }
    public int StickersSentCount { get; set; }
    public int GifsSentCount { get; set; }
    public int PicturesSentCount { get; set; }

    public DateTimeOffset? FirstMessageAt { get; set; }
    public DateTimeOffset? LastMessageAt { get; set; }
}
