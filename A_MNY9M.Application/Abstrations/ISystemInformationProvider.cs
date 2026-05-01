using A_MNY9M.Application.Features.System.BotInformation;

namespace A_MNY9M.Application.Abstrations;

public interface ISystemInformationProvider
{
    SystemInformationDto SystemInformation { get; }
}
