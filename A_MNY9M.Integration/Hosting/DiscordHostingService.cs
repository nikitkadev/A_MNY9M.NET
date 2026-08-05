using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Hosting;

public sealed class DiscordHostingService(
    ILogger<DiscordHostingService> logger,
    IDiscordEventBinder discordEventBinder,
    DiscordSocketClient discordSocketClient) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {

            await discordSocketClient.LoginAsync(
                tokenType: TokenType.Bot,
                token: Environment.GetEnvironmentVariable(EnvironmentVariables.DiscordTokenBot));

            discordEventBinder.Bind();

            await discordSocketClient.StartAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        discordEventBinder.Unbind();

        await discordSocketClient.StopAsync();
    }
}