using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.App;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Hubs
{
    public class MlkAdminAppConfigProvidersHub(
        IJsonMlkAdminAppConfigurationProvider mlkAdminAppConfigurationProvider) : IMlkAdminAppConfigProvidersHub
    {
        public IJsonMlkAdminAppConfigurationProvider MlkAdminAppConfig => mlkAdminAppConfigurationProvider;
    }
}
