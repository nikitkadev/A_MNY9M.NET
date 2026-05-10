using Microsoft.Extensions.Logging;

using MediatR;
using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.BotInformation;
using A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;

namespace A_MNY9M.Integration.Discord.Commands.Router;

public class DiscordSlashCommandRouter(
    ILogger<DiscordSlashCommandRouter> logger,
    IDiscordResponseRenderer<GetBotInfoResult> botInfoResponder,
    IDiscordResponseRenderer<SendHubMessageResult> hubResponder,
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

                    var botInfoCommandResult = await mediator.Send(new GetBotInfoCommand(), cancellationToken);
                    await botInfoResponder.RenderAsync(command, botInfoCommandResult);

                    return;

                case CommandNameConsts.SendhubCommandName:

                    var hubCommandResult  = await mediator.Send(new SendHubMessageCommand(), cancellationToken);
                    await hubResponder.RenderAsync(command, hubCommandResult);

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