namespace A_MNY9M.Infrastructure.Database.Entities.Identity;

public sealed class UserDbEntity
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }

    public string GlobalName { get; set; } = string.Empty;
    public string GuildName { get; set; } = string.Empty;

    public DateTime JoinedAt { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? TgUsername { get; set; }
    
}
