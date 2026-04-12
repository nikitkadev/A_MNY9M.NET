using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M._2_Application.Interfaces.Services;

namespace A_MNY9M._2_Application.Events.GuildAvailable;

class GuildAvailableHandler(
    ILogger<GuildAvailableHandler> logger,
    IGuildInitializationService initializationService) : INotificationHandler<GuildAvailable>
{
    public async Task Handle(GuildAvailable notification, CancellationToken token)
    {
        await initializationService.InitializeAsync(notification.SocketGuild.Id, token);

        logger.LogInformation(
            "Успешная инициализация сервера");
    }
}