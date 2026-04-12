using Microsoft.Extensions.Logging;
using Amnyam.Shared.Dtos;
using Amnyam.Shared.JsonProviders;
using A_MNY9M._1_Core.Enums;
using A_MNY9M._3_Infrastructure.Providers.Models.Guild;
using A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;

namespace A_MNY9M._3_Infrastructure.Providers.Implementations.Configuration.Guild;

public class JsonGuildRolesProvider(string path, ILogger<JsonGuildRolesProvider> logger) : JsonProviderBase<RolesListModel>(path, logger), IJsonGuildRolesProvider
{
    public List<GuildRoleInfo> GuildRoles => GetConfig().GuildRoles;

    public List<ulong> GetColorRolesIds()
    {
        return [..GetConfig().GuildRoles
            .Where(role => role.Type == RoleType.COLOR)
            .Select(role => role.Id)];
    }

    public GuildRoleInfo GetGuildRoleByKey(string key)
    {
        var guildRoles = GetConfig().GuildRoles;

        foreach(var role in guildRoles)
        {
            if (role.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                return role;
        }

        return new GuildRoleInfo();
    }
}