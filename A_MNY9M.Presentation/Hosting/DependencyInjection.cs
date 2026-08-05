using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Application.Abstrations;
using A_MNY9M.Core.Interfaces.Services;
using A_MNY9M.Integration.Confuguration;
using A_MNY9M.Application.Configuration;
using A_MNY9M.Integration.Discord.Client;
using A_MNY9M.Integration.Discord.Managers;
using A_MNY9M.Integration.Discord.Services;
using A_MNY9M.Integration.Discord.Messages;
using A_MNY9M.Integration.Discord.Providers;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Bootstrapping;
using A_MNY9M.Integration.Discord.Components.V2;
using A_MNY9M.Integration.Discord.Commands.Router;
using A_MNY9M.Integration.Discord.Events.Registration;
using A_MNY9M.Integration.Discord.Commands.Responders;
using A_MNY9M.Integration.Discord.Commands.Registration;
using A_MNY9M.Application.Features.System.BotInformation;
using A_MNY9M.Integration.Discord.Components.SelectionMenus;
using A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;
using A_MNY9M.Integration.Discord.Channels;
using A_MNY9M.Integration.Hosting;

namespace A_MNY9M.Presentation.Hosting;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IUserService, DiscordGuildMemberService>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddSingleton<ISystemInformationProvider, SystemInformationProvider>();
        services.AddSingleton<IAnchorMessageProvider, AnchorMessageProvider>();

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
        services.AddSingleton<IDiscordEventBinder, DiscordEventBinder>();
        services.AddSingleton<IDiscordRolesManager, DiscordRolesManager>();
        services.AddSingleton<IDiscordClientWrapper, DiscordClientWrapper>();
        services.AddSingleton<IDiscordMessagesSender, DiscordMessagesSender>();
        services.AddSingleton<IDiscordSlashCommandCreator, MlkSlashCommandCreator>();
        services.AddSingleton<IDiscordSlashCommandRouter, DiscordSlashCommandRouter>();
        services.AddSingleton<IDiscordV2ComponentsBuilder, DiscordV2ComponentsBuilder>();
        services.AddSingleton<IDiscordAnchorMessageUpdater, DiscordAnchorMessageUpdater>();
        services.AddSingleton<IDiscordSelectionMenusBuilder, DiscordSelectionMenusBuilder>();
        services.AddSingleton<IDiscordResponseRenderer<SendHubMessageResult>, HubMessageCommandResponder>();
        services.AddSingleton<IDiscordResponseRenderer<GetBotInfoResult>, GetBotInfoSlashCommandResponder>();
        services.AddSingleton<IDiscordChannelsService, DiscordChannelsService>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly));

        services.AddHostedService<DiscordHostingService>();

        return services;
    }
}