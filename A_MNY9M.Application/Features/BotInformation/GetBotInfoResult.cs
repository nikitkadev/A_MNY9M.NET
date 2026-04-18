using A_MNY9M.Core.Common;

namespace A_MNY9M.Application.Features.BotInformation;

public record GetBotInfoResult(
    string AppName,
    string Author,
    string Company,
    Version Version,
    Uri RepositoryLink,
    DateTimeOffset LastUpdateAt,
    BotStatus BotStatus);