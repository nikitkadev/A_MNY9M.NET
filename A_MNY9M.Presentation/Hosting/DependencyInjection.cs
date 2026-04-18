using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Bootstrapping;
using A_MNY9M.Integration.Discord.Client;

namespace A_MNY9M.Presentation.Hosting;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddDiscordIntegrationServices(
        this IServiceCollection services)
    {
        services.AddSingleton<DiscordSocketClient>(
           sp =>
           {
               return new DiscordSocketClient(
                   new DiscordSocketConfig()
                   {
                       GatewayIntents = GatewayIntents.All
                   });
           });

        services.AddSingleton<IDiscordInitializer, DiscordInitializer>();
        services.AddSingleton<IDiscordClientWrapper, DiscordClientWrapper>();

        return services;
    }

    public static IServiceCollection AddPresentationServices(
        this IServiceCollection services)
    {
        services.AddHostedService<HostedService>();

        return services;
    }
}