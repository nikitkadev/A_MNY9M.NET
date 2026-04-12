namespace A_MNY9M.Core.Entities.Guild.Channel.Voice;

public class VoiceSession
{
    public int Id { get; set; }

    public ulong GuildId { get; set; }
    public ulong MemberDiscordId { get; set; }
    public ulong ChannelDiscordId { get; set; }

    public DateTimeOffset StartingAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? EndingAt { get; set; }

    public TimeSpan Duration
    {
        get
        {
            var duration = (EndingAt ?? DateTimeOffset.UtcNow) - StartingAt;

            return duration < TimeSpan.Zero 
                ? TimeSpan.Zero 
                : duration;
        }
    }

    public void End(DateTimeOffset endingAt)
    {
        if(EndingAt is not null)
            return;

        EndingAt = endingAt;
    }
   
    public bool IsActive => EndingAt is null;
}