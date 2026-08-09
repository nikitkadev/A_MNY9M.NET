namespace A_MNY9M.Core.Entities;

public class GuildMember
{
    public int Id { get; set; }
    public ulong DiscordId { get; set; }

    public string Mention { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;

    public bool IsBanned { get; private set; } 
    public bool IsMuted { get; private set; }

    public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;

    public void Ban() => IsBanned = true;
    public void Unban() => IsBanned = false;
    public void Mute() => IsMuted = true;
    public void Unmute() => IsMuted = false;

}