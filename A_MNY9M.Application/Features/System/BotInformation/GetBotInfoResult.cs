using A_MNY9M.Core.Common;

namespace A_MNY9M.Application.Features.System.BotInformation;

public record GetBotInfoResult(
    string AppName,
    string Author,
    string Company,
    Version Version,
    Uri RepositoryLink,
    Uri AboutCommandsLink,
    Uri AboutEventsLink,
    DateTimeOffset LastUpdateAt,
    BotStatus BotStatus);