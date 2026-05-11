namespace A_MNY9M.Integration.Discord.Options;

public class DiscordOption
{
    public MalenkieGuild MalenkieGuild { get; set; } = new();
    public AppEmotes AppEmotes { get; set; } = new();
    public AnchorChannels AnchorChannels { get; set; } = new();
    public AnchorMessages AnchorMessages { get; set; } = new();
}

public class MalenkieGuild
{
    public string Name { get; set; } = string.Empty;
    public ulong DiscordId { get; set; }
}

public class AnchorChannels
{
    public ulong HubDiscordId { get; set; }
}

public class AnchorMessages
{
    public ulong WelcomeDiscordId { get; set; }
}

public class AppEmotes
{
    public ulong GitHubIconDiscordId { get; set; }
    public ulong DotnetIconDiscordId { get; set; }
    public ulong JettLoveIconDiscordId { get; set; }
    public ulong SageLoveIconDiscordId { get; set; }
    public ulong FadeLoveIconDiscordId { get; set; }
    public ulong PixelGreenStarDiscordId { get; set; }
    public ulong PixelRedStarDiscordId { get; set; }
    public ulong PixelOrangeStarDiscordId { get; set; }


}