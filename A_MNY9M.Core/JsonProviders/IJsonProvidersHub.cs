using Amnyam._3_Infrastructure.Providers.Interfaces.Configuration.Guild;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.App;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;


namespace A_MNY9M._1_Core.JsonProviders;

public interface IJsonProvidersHub
{
    IJsonGuildConfigProvidersHub GuildConfigProvidersHub { get; }
    IJsonMessageProvidersHub MessageProvidersHub { get; }
    IJsonMlkAdminAppConfigurationProvider AppConfigProvidersHub { get; }
}
