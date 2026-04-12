using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.App;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

public interface IMlkAdminAppConfigProvidersHub
{
    IJsonMlkAdminAppConfigurationProvider MlkAdminAppConfig { get; }

}
