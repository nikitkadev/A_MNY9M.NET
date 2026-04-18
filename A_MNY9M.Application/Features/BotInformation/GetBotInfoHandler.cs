using MediatR;
using Microsoft.Extensions.Logging;

namespace A_MNY9M.Application.Features.BotInformation;

public class GetBotInfoHandler(
    ILogger<GetBotInfoHandler> logger) : IRequestHandler<GetBotInfoCommand, GetBotInfoResult>
{
    public async Task<GetBotInfoResult> Handle(
        GetBotInfoCommand request, 
        CancellationToken cancellationToken)
    {
        //TODO: Десериализация из JSON мета-информации, формирование результат, возврат результата.
    }
}