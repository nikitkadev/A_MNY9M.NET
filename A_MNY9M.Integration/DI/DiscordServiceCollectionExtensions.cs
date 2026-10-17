using A_MNY9M.Application;
using A_MNY9M.Integration.Discord.Commands;
using A_MNY9M.Integration.Discord.Commands.Handlers;
using A_MNY9M.Integration.Discord.Content.Models;
using A_MNY9M.Integration.Discord.Events.Binder;
using A_MNY9M.Integration.Discord.Events.Handlers;
using A_MNY9M.Integration.Hosting;
using A_MNY9M.Integration.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;


namespace A_MNY9M.Integration.DI;

public static class DiscordServiceCollectionExtensions
{
    public static IServiceCollection AddAmnyamDiscordServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        RegisterDiscordEventHandlers(services);
        RegisterDiscordCommands(services);
        RegisterDiscordInitServices(services);
        RegisterAmnyamServices(services);
        ConfigureDiscordOptions(services, configuration);

        return services;
    }

    private static void RegisterAmnyamServices(
        IServiceCollection services)
    {
        services.AddSingleton<GuildLogService>();
        services.AddSingleton<DiscordChannelService>();
    }

    private static void RegisterDiscordInitServices(
        IServiceCollection services)
    {
        services.AddHostedService<DiscordHostingService>();

        services.AddSingleton(
            _ => new DiscordSocketClient(
                new DiscordSocketConfig()
                {
                    GatewayIntents =
                        GatewayIntents.GuildVoiceStates |
                        GatewayIntents.Guilds |
                        GatewayIntents.AutoModerationActionExecution |
                        GatewayIntents.GuildMembers |
                        GatewayIntents.GuildEmojis |
                        GatewayIntents.GuildBans
                }));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly);
            config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly);
        });
    }

    private static void ConfigureDiscordOptions(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<DiscordConfigurationOption>()
            .Bind(configuration
                .GetSection(DiscordConfigurationOption.SectionName));

        services
            .AddOptions<GuildConfigurationOption>()
            .Bind(configuration
                .GetSection(GuildConfigurationOption.SectionName));
    }

    private static void RegisterDiscordEventHandlers(
        IServiceCollection services)
    {
        services.AddSingleton<DiscordEventBinder>();
        services.AddSingleton<UserVoiceStateUpdatedHandler>();
        services.AddSingleton<GuildAvailabeHandler>();
        services.AddSingleton<SlashCommandExecutedHandler>();
    }

    private static void RegisterDiscordCommands(
        IServiceCollection services)
    {
        services.AddSingleton<GuildCommandRegistrar>();
        services.AddSingleton<SetSourceVoiceChannelCommandHandler>();
    }

}