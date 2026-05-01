namespace A_MNY9M.Integration.Discord.Options;

public class DiscordOption
{
    public MalenkieGuild MalenkieGuild { get; set; } = new();
    public AppEmotes AppEmotes { get; set; } = new();
}

public class MalenkieGuild
{
    public string Name { get; set; } = string.Empty;
    public ulong DiscordId { get; set; } 
}

public class AppEmotes
{
    public ulong GitHubIconDiscordId { get; set; }
    public ulong DotnetIconDiscordId { get; set; }
}