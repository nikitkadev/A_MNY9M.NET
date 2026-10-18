namespace A_MNY9M.Infrastructure.Database.Entities.Channels;

public sealed class SourceVoiceChannelDbEntity
{
    public Guid Id { get; set; }
    
    public ulong DiscordId { get; set; }
    public ulong InitiatorDiscordId { get; set; }
    public ulong? GuildCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? SetAt { get; set; }
}