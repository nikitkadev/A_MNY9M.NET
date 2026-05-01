using Microsoft.Extensions.Logging;

using MediatR;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Events.Handlers.Ready;

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
    }

    public void Unbind()
    {
        discordClientWrapper.DiscordSocketClient.Ready -= OnReady;
        discordClientWrapper.DiscordSocketClient.Log -= OnLog;
        discordClientWrapper.DiscordSocketClient.SlashCommandExecuted -= OnSlashCommandExecuted;
    }

    private async Task OnSlashCommandExecuted(SocketSlashCommand slashCommand)
    {
        try
        {
            await slashCommand.DeferAsync(ephemeral: true);

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
}