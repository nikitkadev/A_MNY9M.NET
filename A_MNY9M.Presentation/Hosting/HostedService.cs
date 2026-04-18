using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using A_MNY9M.Presentation.Configuration;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Presentation.Hosting;

public class HostedService(
    ILogger<HostedService> logger,
    IDiscordClientWrapper discordClientWrapper,
    IDiscordInitializer discordInitializer,
    IOptions<DiscordBotConfiguration> options) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await discordClientWrapper.DiscordSocketClient.LoginAsync(
                Discord.TokenType.Bot,
                options.Value.Token);

            await discordInitializer.InitializeAsync();
            await discordClientWrapper.DiscordSocketClient.StartAsync();
        }
        catch(Exception ex) 
        {
            logger.LogCritical(
                ex,
                "Критическая ошибка при попытке запуска {HostedService}",
                nameof(HostedService));

            throw;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await discordClientWrapper.DiscordSocketClient.StopAsync();
    }
}