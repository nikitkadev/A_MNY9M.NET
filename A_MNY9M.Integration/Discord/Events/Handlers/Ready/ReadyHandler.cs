using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Events.Handlers.Ready;

public class ReadyHandler(
    ILogger<ReadyHandler> logger,
    IDiscordSlashCommandCreator discordSlashCommandCreator) : INotificationHandler<ReadyNotification>
{
    public async Task Handle(
        ReadyNotification notification, 
        CancellationToken cancellationToken)
    {
        try
        {
            await discordSlashCommandCreator.CreateGuildSlashCommandAsync();
        }
        catch(Exception ex)
        {
            logger.LogCritical(
                "Критическая ошибка при попытке создать слеш-команды для сервера. Сообщение: {ErrorMessage}",
                ex.Message);

            throw;
        }
    }
}