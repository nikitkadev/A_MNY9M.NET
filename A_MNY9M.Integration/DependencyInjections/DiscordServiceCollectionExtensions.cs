using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Application;
using A_MNY9M.Integration.Hosting;
using A_MNY9M.Integration.Discord.Content.Models;
using A_MNY9M.Integration.Discord.Events.Binder;
using A_MNY9M.Integration.Discord.Events.Handlers;
using A_MNY9M.Integration.Services;


namespace A_MNY9M.Integration.Extensions;

public static class DiscordServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordServices(this IServiceCollection services)
    {
        services.AddHostedService<DiscordHostingService>();
        services.AddSingleton<GuildLogService>();
        services.AddSingleton<DiscordEventBinder>();
        services.AddSingleton<DiscordChannelService>();
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents =
                GatewayIntents.GuildVoiceStates |
                GatewayIntents.Guilds |
                GatewayIntents.AutoModerationActionExecution |
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildEmojis |
                GatewayIntents.GuildBans
        }));

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly));

        return services;
    }

    public static IServiceCollection ConfigureIntegrationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DiscordConfigurationOption>()
            .Bind(configuration.GetSection(DiscordConfigurationOption.SectionName));

        services.AddOptions<GuildConfigurationOption>()
            .Bind(configuration.GetSection(GuildConfigurationOption.SectionName));

        return services;
    }

    public static IServiceCollection AddHandlerServices(this IServiceCollection services)
    {
        services.AddSingleton<UserVoiceStateUpdatedHandler>();

        return services;
    }

}