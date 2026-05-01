using Microsoft.Extensions.Options;

using A_MNY9M.Application.Abstrations;
using A_MNY9M.Application.Features.System.BotInformation;

namespace A_MNY9M.Integration.Discord.Providers;

public class SystemInformationProvider(
    IOptions<SystemInformationDto> options) : ISystemInformationProvider
{
    public SystemInformationDto SystemInformation => options.Value;
}
