using A_MNY9M._3_Infrastructure.Providers.Models.Guild;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;
public interface IJsonGuildChannelsProvider 
{
    VoiceChannels VoiceChannels { get; }
    TextChannels TextChannels { get; }
}