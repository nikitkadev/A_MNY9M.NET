using MediatR;

using A_MNY9M.Application.Abstrations;

namespace A_MNY9M.Application.Features.System.BotInformation;

public class GetBotInfoHandler(
    ISystemInformationProvider systemInformationProvider) : IRequestHandler<GetBotInfoCommand, GetBotInfoResult>
{
    public Task<GetBotInfoResult> Handle(
        GetBotInfoCommand request, 
        CancellationToken cancellationToken)
    {
        var amnyamSystemInfo = systemInformationProvider.SystemInformation;

        return Task.FromResult(
            new GetBotInfoResult(
                AppName: amnyamSystemInfo.AppName,
                Author: amnyamSystemInfo.Author.Username,
                Company: amnyamSystemInfo.Company.Name, 
                Version: new Version(amnyamSystemInfo.Version), 
                RepositoryLink: new Uri(amnyamSystemInfo.Links.RepositoryLink), 
                AboutCommandsLink: new Uri(amnyamSystemInfo.Links.AboutCommandsLink),
                AboutEventsLink: new Uri(amnyamSystemInfo.Links.AboutEventsLink),
                LastUpdateAt: new DateTimeOffset(DateTime.Parse(amnyamSystemInfo.LastUpdateAt)), 
                BotStatus: Core.Common.BotStatus.Running));
    }
}