using Microsoft.Extensions.Logging;

using MediatR;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Events.Handlers.Ready;
using A_MNY9M.Integration.Discord.Events.Handlers.GuildAvaliable;
using A_MNY9M.Integration.Discord.Events.Handlers.ButtonExecuted;

namespace A_MNY9M.Integration.Discord.Events.Registration;

public class DiscordEventBinder(
    ILogger<DiscordEventBinder> logger,
    IMediator mediator,
    IDiscordClientWrapper discordClientWrapper,
    IDiscordSlashCommandRouter router) : IDiscordEventBinder
{
    public void Bind()
    {
        discordClientWrapper.DiscordSocketClient.Ready += OnReady;
        discordClientWrapper.DiscordSocketClient.Log += OnLog;
        discordClientWrapper.DiscordSocketClient.SlashCommandExecuted += OnSlashCommandExecuted;
        discordClientWrapper.DiscordSocketClient.GuildAvailable += OnGuildAvailable;
        discordClientWrapper.DiscordSocketClient.ButtonExecuted += OnButtonExecuted;
    }

    public void Unbind()
    {
        discordClientWrapper.DiscordSocketClient.Ready -= OnReady;
        discordClientWrapper.DiscordSocketClient.Log -= OnLog;
        discordClientWrapper.DiscordSocketClient.SlashCommandExecuted -= OnSlashCommandExecuted;
        discordClientWrapper.DiscordSocketClient.GuildAvailable -= OnGuildAvailable;
    }

    private async Task OnSlashCommandExecuted(SocketSlashCommand slashCommand)
    {
        try
        {
            await router.RouteAsync(
                slashCommand, 
                CancellationToken.None);
        }
        catch(Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при попытке обработать событие " +
                "{discordClientWrapper.DiscordSocketClient.SlashCommandExecuted}",
                nameof(discordClientWrapper.DiscordSocketClient.SlashCommandExecuted));
        }
    }

    private async Task OnReady()
    {
        try
        {
            await mediator.Publish(new ReadyNotification());
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при попытке обработать событие " +
                "{discordClientWrapper.DiscordSocketClient.Ready}",
                nameof(discordClientWrapper.DiscordSocketClient.Ready));
        }
    }

    private Task OnLog(LogMessage logMessage)
    {
        logger.LogInformation(logMessage.Message);
        return Task.CompletedTask;
    }

    private async Task OnGuildAvailable(SocketGuild socketGuild)
    {
        try
        {
            await mediator.Publish(new GuildAvaliableNotification(socketGuild));
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при попытке обработать событие " +
                "{discordClientWrapper.DiscordSocketClient.GuildAvailable}",
                nameof(discordClientWrapper.DiscordSocketClient.GuildAvailable));
        }
    }

    private async Task OnButtonExecuted(SocketMessageComponent component)
    {
        try
        {
            await mediator.Publish(new ButtonExecutedNotification(component));
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при попытке обработать событие " +
                "{discordClientWrapper.DiscordSocketClient.ButtonExecuted}",
                nameof(discordClientWrapper.DiscordSocketClient.ButtonExecuted));
        }
    }
}