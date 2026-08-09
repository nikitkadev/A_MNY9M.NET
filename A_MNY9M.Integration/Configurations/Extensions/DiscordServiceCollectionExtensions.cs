using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Application;
using A_MNY9M.Integration.Hosting;
using A_MNY9M.Integration.Discord.Services;
using A_MNY9M.Integration.Discord.Events.Binder;
using A_MNY9M.Integration.Configurations.Options;

namespace A_MNY9M.Integration.Configurations.Extensions;

public static class DiscordServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<DiscordHostingService>();

        services.AddSingleton<DiscordEventBinder>();
        services.AddSingleton<DiscordTextMessageSender>();
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig() { 
            GatewayIntents = 
                GatewayIntents.GuildVoiceStates | 
                GatewayIntents.Guilds |
                GatewayIntents.AutoModerationActionExecution | 
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildEmojis |
                GatewayIntents.GuildBans}));

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly));

        services.Configure<DiscordConfiguration>(configuration.GetSection("DiscordConfiguration"));

        return services;
    }
}