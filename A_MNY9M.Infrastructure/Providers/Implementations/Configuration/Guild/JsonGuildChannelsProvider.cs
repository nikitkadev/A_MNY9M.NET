using Microsoft.Extensions.Logging;
using Amnyam.Shared.JsonProviders;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;
using A_MNY9M._3_Infrastructure.Providers.Models.Guild;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Configuration.Guild;

public class JsonGuildChannelsProvider(string path, ILogger<JsonGuildChannelsProvider> logger) : JsonProviderBase<RootChannelsModel>(path, logger), IJsonGuildChannelsProvider
{
    public VoiceChannels VoiceChannels => GetConfig().VoiceChannels;

    public TextChannels TextChannels => GetConfig().TextChannels;
}
