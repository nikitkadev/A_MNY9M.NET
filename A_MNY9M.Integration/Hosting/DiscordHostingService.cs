using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Events.Binder;
using A_MNY9M.Integration.Discord.Content.Models;

namespace A_MNY9M.Integration.Hosting;

public sealed class DiscordHostingService(
    IOptions<DiscordConfigurationOption> options,
    DiscordEventBinder discordEventBinder,
    DiscordSocketClient discordSocketClient) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await discordSocketClient.LoginAsync(
            tokenType: TokenType.Bot,
            token: Environment.GetEnvironmentVariable(options.Value.TokenEnvironmentVariables.Development));

        discordEventBinder.Bind();

        await discordSocketClient.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        discordEventBinder.Unbind();

        await discordSocketClient.StopAsync();
    }
}