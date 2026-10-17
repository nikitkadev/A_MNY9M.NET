using Microsoft.Extensions.Logging;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Events.Handlers;

namespace A_MNY9M.Integration.Discord.Events.Binder;

public class DiscordEventBinder(
    ILogger<DiscordEventBinder> logger,
    DiscordSocketClient discordSocketClient,
    GuildAvailabeHandler guildAvailabeHandler,
    UserVoiceStateUpdatedHandler userVoiceStateUpdatedHandler)
{
    public void Bind()
    {
        discordSocketClient.Log += OnLog;
        discordSocketClient.GuildAvailable += guildAvailabeHandler.HandleAsync;
        discordSocketClient.UserVoiceStateUpdated += userVoiceStateUpdatedHandler.HandleAsync;
    }

    public void Unbind()
    {
        discordSocketClient.Log -= OnLog;
        discordSocketClient.GuildAvailable -= guildAvailabeHandler.HandleAsync;
        discordSocketClient.UserVoiceStateUpdated -= userVoiceStateUpdatedHandler.HandleAsync;
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