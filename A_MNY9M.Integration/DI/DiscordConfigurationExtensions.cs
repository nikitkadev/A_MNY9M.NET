using Microsoft.Extensions.Configuration;

namespace A_MNY9M.Integration.DI;

public static class DiscordConfigurationExtensions
{
    public static IConfigurationBuilder AddAmnyamDiscordConfiguration(
        this IConfigurationBuilder configurationBuilder)
    {
        ConfigureOptionSources(configurationBuilder);

        return configurationBuilder;
    }

    private static void ConfigureOptionSources(
        IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonFile(
            path: Path.Combine(
                AppContext.BaseDirectory,
                "Discord/Content/Data/discord_configuration.json"),
            optional: false,
            reloadOnChange: true);

        configurationBuilder.AddJsonFile(
            path: Path.Combine(
                AppContext.BaseDirectory,
                "Discord/Content/Data/guild_configuration.json"),
            optional: false,
            reloadOnChange: true);
    }
}
