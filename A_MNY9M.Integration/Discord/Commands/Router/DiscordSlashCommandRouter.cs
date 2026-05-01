using Microsoft.Extensions.Logging;

using MediatR;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.BotInformation;

namespace A_MNY9M.Integration.Discord.Commands.Router;

public class DiscordSlashCommandRouter(
    ILogger<DiscordSlashCommandRouter> logger,
    IDiscordResponseRenderer<GetBotInfoResult> botInfoResponder,
    IMediator mediator) : IDiscordSlashCommandRouter
{

    public async Task RouteAsync(
        SocketSlashCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            switch (command.CommandName)
            {
                case CommandNameConsts.BotInfoCommandName:

                    var result = await mediator.Send(new GetBotInfoCommand(), cancellationToken);

                    await botInfoResponder.RenderAsync(command, result);

                    return;

                default:

                    return;
            }

        }
        catch (Exception ex)
        {
            logger.LogCritical(
                ex,
                "Критическая ошибка при попытке вызвать обработчик команды");

            throw;
        }
    }
}