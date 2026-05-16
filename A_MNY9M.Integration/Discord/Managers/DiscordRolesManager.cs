using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Managers;

public class DiscordRolesManager(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordRolesManager
{
    public async Task UploadUserSelectedCategoryRolesAsync(
        SocketGuildUser user, 
        IReadOnlyCollection<string> values)
    {
        if (values.Contains(SelectionMenuValues.RemoveAll))
        {
            var rolesForRemove = malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds.Select(x => x.Value);

            await user.RemoveRolesAsync(rolesForRemove);
            return;
        }

        await user.AddRolesAsync(values.Select(x => ulong.Parse(x)));
    }

    public async Task UploadUserSelectedNameColorAsync(
        SocketGuildUser user,
        IReadOnlyCollection<string> values)
    {
        var rolesForRemove = malenkieOptions.Value.TestSelectedColorRoles.RoleIds.Select(x => x.Value);

        await user.RemoveRolesAsync(rolesForRemove);

        if (values.Contains(SelectionMenuValues.RemoveAll))
        {
            return;
        }

        await user.AddRoleAsync(ulong.Parse(values.ElementAt(0)));
    }

    public async Task SetRolesToUserAsync(
        ulong userId, 
        List<ulong> roleIds)
    {
        var user = clientWrapper.MlkGuild.GetUser(userId);
        await user.AddRolesAsync(roleIds);
    }
}