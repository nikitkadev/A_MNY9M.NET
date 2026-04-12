using Microsoft.Extensions.Logging;
using Amnyam.Shared.JsonProviders;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Messages;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Configuration.Messages;

public class JsonColorsMessageProvider(string path, ILogger<JsonColorsMessageProvider> logger) 
    : JsonMessageProviderBase<string>(path, logger), IColorMessageProvider
{
   
}

public class JsonRulesMessageProvider(string path, ILogger<JsonRulesMessageProvider> logger) 
    : JsonMessageProviderBase<string>(path, logger), IRulesMessageProvider
{
   
}

public class JsonHubMessageProvider(string path, ILogger<JsonHubMessageProvider> logger) 
    : JsonMessageProviderBase<string>(path, logger), IHubMessageProvider
{

}

public class JsonWelcomeMessageProvider(string path, ILogger<JsonWelcomeMessageProvider> logger) 
    : JsonMessageProviderBase<string>(path, logger), IWelcomeMessageProvider
{

}