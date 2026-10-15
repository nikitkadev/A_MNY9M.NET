namespace A_MNY9M.Infrastructure.Database.Entities.Channels;

public sealed class AutoVoiceChannelDbEntity
{
    public int Id { get; set; }

    public ulong LeaderDiscordId { get; set; }
    public ulong DiscordId { get; set; }
    public DateTime CreatedAt { get; set; }

}
