namespace A_MNY9M.Infrastructure.Database.Entities.Channels;

public sealed class SourceVoiceChannelDbEntity
{
    public int Id { get; set; }

    public ulong DiscordId { get; set; }
    public string Name { get; set; } = string.Empty;
}