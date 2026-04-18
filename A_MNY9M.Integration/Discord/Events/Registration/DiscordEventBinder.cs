using Microsoft.Extensions.Logging;

using MediatR;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Events.Handlers.Ready;

namespace A_MNY9M.Integration.Discord.Events.Registration;

public class DiscordEventBinder(
    ILogger<DiscordEventBinder> logger,
    IMediator mediator,
    IDiscordClientWrapper discordClientWrapper,
    IDiscordCommandRouter router) : IDiscordEventBinder
{
    public void Bind()
    {
        discordClientWrapper.DiscordSocketClient.SlashCommandExecuted += OnSlashCommandExecuted;
        discordClientWrapper.DiscordSocketClient.Ready += OnReady;
    }

    public void Unbind()
    {
        discordClientWrapper.DiscordSocketClient.SlashCommandExecuted -= OnSlashCommandExecuted;
        discordClientWrapper.DiscordSocketClient.Ready -= OnReady;
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
}