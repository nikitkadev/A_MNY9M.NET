namespace A_MNY9M.Integration.Discord.Content.Models;

public sealed class GuildConfigurationOption
{
    public const string SectionName = "Guild";

    public ulong DiscordId { get; set; }
    public ulong TestDiscordId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, ulong> Channels { get; set; } = null!;

}
