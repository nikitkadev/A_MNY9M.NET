using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Messages;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

public interface IJsonMessageProvidersHub
{
    IWelcomeMessageProvider Welcome { get; }
    IHubMessageProvider Hub { get; }
    IRulesMessageProvider Rules { get; }
    IColorMessageProvider Color { get; }
}
