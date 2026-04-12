using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Messages;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Hubs;

public class JsonMessageProvidersHub(
    IWelcomeMessageProvider welcomeMessage,
    IHubMessageProvider hubMessage,
    IRulesMessageProvider rulesMessage,
    IColorMessageProvider colorMessage) : IJsonMessageProvidersHub
{
    public IWelcomeMessageProvider Welcome => welcomeMessage;
    public IHubMessageProvider Hub => hubMessage;
    public IRulesMessageProvider Rules => rulesMessage;
    public IColorMessageProvider Color => colorMessage;
}
