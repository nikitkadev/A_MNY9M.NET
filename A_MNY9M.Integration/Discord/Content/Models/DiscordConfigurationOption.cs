namespace A_MNY9M.Integration.Discord.Content.Models;

public sealed class DiscordConfigurationOption
{
    public const string SectionName = "Discord";
    public TokenEnvironmentVariables TokenEnvironmentVariables { get; set; } = new();

}

public sealed class TokenEnvironmentVariables
{
    public string Production { get; set; } = string.Empty;
    public string Development { get; set; } = string.Empty;
}