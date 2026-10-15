using Microsoft.Extensions.Configuration;

namespace A_MNY9M.Integration.Extensions;

public static class DiscordConfigurationExtension
{
    public static IConfigurationBuilder AddDiscordJsonSources(
        this IConfigurationBuilder configuration)
    {
        configuration.AddJsonFile(
            path: Path.Combine(
                AppContext.BaseDirectory,
                "Discord/Content/Data/discord_configuration.json"),
            optional: false,
            reloadOnChange: true);

        configuration.AddJsonFile(
            path: Path.Combine(
                AppContext.BaseDirectory,
                "Discord/Content/Data/guild_configuration.json"),
            optional: false,
            reloadOnChange: true);

        return configuration;
    }
}
