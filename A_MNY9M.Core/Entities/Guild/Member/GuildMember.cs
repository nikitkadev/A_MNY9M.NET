namespace A_MNY9M.Core.Entities.Guild.Member;

public class GuildMember
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }

    public bool IsGuildAuthorized { get; set; }

    public string DiscordUsername { get; set; } = string.Empty;
    public string GuildDisplayName { get; set; } = string.Empty;
    public string GuildDisplayAvatarUrl { get; set; } = string.Empty;

    public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;
}