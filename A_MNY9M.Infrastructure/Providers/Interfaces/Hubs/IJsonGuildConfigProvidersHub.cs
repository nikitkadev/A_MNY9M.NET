using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;
using Amnyam._3_Infrastructure.Providers.Interfaces.Configuration.App;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

public interface IJsonGuildConfigProvidersHub
{
    IJsonGuildCategoriesProvider Categories { get; }
    IJsonGuildChannelsProvider Channels { get; }
    IJsonGuildConfigurationProvider GuildConfig { get; }
    IJsonGuildRolesProvider Roles { get; }
}
