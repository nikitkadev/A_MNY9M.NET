using Microsoft.Extensions.Options;

using A_MNY9M.Application.Abstrations;
using A_MNY9M.Application.Features.System.AnchorMessages;

namespace A_MNY9M.Integration.Discord.Providers;

public class AnchorMessageProvider(
    IOptions<AnchorMessagesContent> options) : IAnchorMessageProvider
{
    public AnchorMessagesContent AnchorMessages => options.Value;
}
