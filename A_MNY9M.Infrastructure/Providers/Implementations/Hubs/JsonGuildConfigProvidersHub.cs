using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Hubs;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Hubs;

public class JsonGuildConfigProvidersHub(
    IJsonGuildCategoriesProvider categoriesProvider,
    IJsonGuildChannelsProvider channelsProvider,
    IJsonGuildConfigurationProvider guildConfigurationProvider,
    IJsonGuildRolesProvider guildRolesProvider) : IJsonGuildConfigProvidersHub
{
    public IJsonGuildCategoriesProvider Categories => categoriesProvider;
    public IJsonGuildChannelsProvider Channels => channelsProvider;
    public IJsonGuildConfigurationProvider GuildConfig => guildConfigurationProvider;
    public IJsonGuildRolesProvider Roles => guildRolesProvider;
}
