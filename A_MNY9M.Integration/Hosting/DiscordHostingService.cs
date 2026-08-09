using Microsoft.Extensions.Hosting;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Core.Common.Constants;
using A_MNY9M.Integration.Discord.Events.Binder;

namespace A_MNY9M.Integration.Hosting;

public sealed class DiscordHostingService(
    DiscordEventBinder discordEventBinder,
    DiscordSocketClient discordSocketClient) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await discordSocketClient.LoginAsync(
            tokenType: TokenType.Bot,
            token: Environment.GetEnvironmentVariable(DiscordConfigurationConstants.TestingTokenEnvironmentName));

        discordEventBinder.Bind();

        await discordSocketClient.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        discordEventBinder.Unbind();

        await discordSocketClient.StopAsync();
    }
}