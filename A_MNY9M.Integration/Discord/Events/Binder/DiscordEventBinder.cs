using Microsoft.Extensions.Logging;

using Discord;
using Discord.WebSocket;
using Microsoft.IdentityModel.Tokens;

namespace A_MNY9M.Integration.Discord.Events.Binder;

public class DiscordEventBinder(
    ILogger<DiscordEventBinder> logger,
    DiscordSocketClient discordSocketClient)
{
    public void Bind()
    {
        discordSocketClient.Log += OnLog;
    }

    public void Unbind()
    {
        discordSocketClient.Log -= OnLog;
    }


    private Task OnLog(LogMessage logMessage)
    {
        switch (logMessage.Severity)
        {
            case LogSeverity.Info:

                logger.LogInformation(
                    logMessage.Exception,
                    "[A_MNY9M] {Source} {Message}",
                    logMessage.Source,
                    logMessage.Message);

                break;

            case LogSeverity.Warning:

                logger.LogWarning(
                    logMessage.Exception,
                    "[A_MNY9M] {Source} {Message}",
                    logMessage.Source,
                    logMessage.Message);

                break;

            case LogSeverity.Debug:

                logger.LogDebug(
                    logMessage.Exception,
                    "[A_MNY9M] {Source} {Message}",
                    logMessage.Source,
                    logMessage.Message);

                break;

            case LogSeverity.Critical:

                logger.LogCritical(
                     logMessage.Exception,
                     "[A_MNY9M] {Source} {Message}",
                     logMessage.Source,
                     logMessage.Message);

                break;

            case LogSeverity.Error:

                logger.LogError(
                    logMessage.Exception,
                    "[A_MNY9M] {Source} {Message}",
                    logMessage.Source,
                    logMessage.Message);

                break;

            case LogSeverity.Verbose:

                logger.LogWarning(
                    logMessage.Exception,
                    "[A_MNY9M] {Source} {Message}",
                    logMessage.Source,
                    logMessage.Message);

                break;
        }

        return Task.CompletedTask;
    }

}