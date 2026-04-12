using Amnyam.Shared.Dtos;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;

public interface IJsonGuildRolesProvider
{
    List<GuildRoleInfo> GuildRoles { get; }
    List<ulong> GetColorRolesIds();
    GuildRoleInfo GetGuildRoleByKey(string roleKey);
}
