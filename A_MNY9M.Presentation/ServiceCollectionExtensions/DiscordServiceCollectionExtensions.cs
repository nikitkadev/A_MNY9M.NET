using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration;
using A_MNY9M.Integration.Hosting;
using A_MNY9M.Integration.Discord.Events.Binder;
using A_MNY9M.Application;

namespace A_MNY9M.Presentation.ServiceCollectionExtensions;

public static class DiscordServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordServices(this IServiceCollection services)
    {
        services.AddSingleton<DiscordEventBinder>();
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig() { 
            GatewayIntents = 
                GatewayIntents.GuildVoiceStates | 
                GatewayIntents.AutoModerationActionExecution | 
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildEmojis |
                GatewayIntents.GuildBans}));

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly));

        services.AddHostedService<DiscordHostingService>();

        return services;
    }
}
