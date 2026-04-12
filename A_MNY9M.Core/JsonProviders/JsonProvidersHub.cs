using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.App;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

namespace A_MNY9M._1_Core.JsonProviders;

public class JsonProvidersHub(
    IJsonGuildConfigProvidersHub guildConfigProvidersHub,
    IJsonMessageProvidersHub messageProvidersHub,
    IJsonMlkAdminAppConfigurationProvider mlkAdminAppConfigurationProvider) : IJsonProvidersHub
{
    public IJsonGuildConfigProvidersHub GuildConfigProvidersHub => guildConfigProvidersHub;
    public IJsonMessageProvidersHub MessageProvidersHub => messageProvidersHub;
    public IJsonMlkAdminAppConfigurationProvider AppConfigProvidersHub => mlkAdminAppConfigurationProvider;
}
