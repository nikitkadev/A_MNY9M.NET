namespace A_MNY9M.Core.Entities.Metrics;

public class MemberVoiceSessionMetric
{
    public int Id { get; set; }

    public ulong GuildDiscordId { get; set; }
    public ulong MemberDiscordId { get; set; }

    public long TotalVoiceSeconds { get; set; }

    public long TodayVoiceSeconds { get; set; }
    public long WeekVoiceSeconds { get; set; }
    public long MonthVoiceSeconds { get; set; }

    public DateTimeOffset? LastSessionAt { get; set; }
}