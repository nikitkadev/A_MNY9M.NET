using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Presentation.Options;
using A_MNY9M.Application.Abstrations;
using A_MNY9M.Integration.Confuguration;
using A_MNY9M.Application.Configuration;
using A_MNY9M.Integration.Discord.Client;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Providers;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Bootstrapping;
using A_MNY9M.Integration.Discord.Components.V2;
using A_MNY9M.Integration.Discord.AnchorMessages;
using A_MNY9M.Integration.Discord.Commands.Router;
using A_MNY9M.Integration.Discord.Events.Registration;
using A_MNY9M.Integration.Discord.Commands.Responders;
using A_MNY9M.Integration.Discord.Commands.Registration;
using A_MNY9M.Application.Features.System.BotInformation;
using A_MNY9M.Application.Features.System.AnchorMessages;
using A_MNY9M.Integration.Discord.Components.SelectionMenus;
using A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;
using A_MNY9M.Integration.Discord.Managers;

namespace A_MNY9M.Presentation.Hosting;

public static class DependencyInjection
{
    public static IServiceCollection AddAppConfigurations(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddOptions<AppOption>()
            .Bind(configuration.GetSection("AppOption"))
            .Validate(x => !string.IsNullOrEmpty(x.BotToken), "Токен не должен быть пустым")
            .ValidateOnStart();

        service.AddOptions<DiscordOption>()
            .Bind(configuration.GetSection("DiscordOption"))
            .Validate(x => x.MalenkieGuild.DiscordId != 0, "DiscordId сервера не должен быть равен нулю")
            .ValidateOnStart();

        service.AddOptions<SystemInformationDto>()
            .Bind(configuration.GetSection("SystemInformation"))
            .ValidateOnStart();

        service.AddOptions<AnchorMessagesContent>()
            .Bind(configuration.GetSection("AnchorMessagesContent"))
            .ValidateOnStart();

        return service;
    }

    public static IServiceCollection AddCoreServices(
        this IServiceCollection services)
    {
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
        services.AddSingleton<IDiscordClientWrapper, DiscordClientWrapper>();
        services.AddSingleton<IDiscordEventBinder, DiscordEventBinder>();
        services.AddSingleton<IDiscordSlashCommandRouter, DiscordSlashCommandRouter>();
        services.AddSingleton<IDiscordSlashCommandCreator, MlkSlashCommandCreator>();
        services.AddSingleton<IDiscordResponseRenderer<GetBotInfoResult>, GetBotInfoSlashCommandResponder>();
        services.AddSingleton<IDiscordResponseRenderer<SendHubMessageResult>, HubMessageCommandResponder>();
        services.AddSingleton<IDiscordV2ComponentsBuilder, DiscordV2ComponentsBuilder>();
        services.AddSingleton<IDiscordAnchorMessageUpdater, DiscordAnchorMessageUpdater>();
        services.AddSingleton<IDiscordSelectionMenusBuilder, DiscordSelectionMenusBuilder>();
        services.AddSingleton<IDiscordRolesManager, DiscordRolesManager>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IntegrationMarker).Assembly));

        return services;
    }

    public static IServiceCollection AddPresentationServices(
        this IServiceCollection services)
    {
        services.AddHostedService<HostedService>();

        return services;
    }
}