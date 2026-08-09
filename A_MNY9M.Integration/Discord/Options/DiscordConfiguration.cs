namespace A_MNY9M.Integration.Discord.Options;

public sealed class DiscordConfiguration
{
    public ulong GuildId { get; set; }
    public Channels Channels { get; set; } = new();
    public Emotes Emotes { get; set; } = new();
}

public sealed class Channels
{
    public ulong Hub { get; set; }
    public ulong Logs { get; set; }
    public ulong BotCommands { get; set; }
}

public sealed class Emotes
{
    public ulong RuleDot { get; set; }
}