using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.Managers;

public class DiscordRolesManager(
    IOptions<DiscordOption> discordAppOptions) : IDiscordRolesManager
{
    public async Task UploadUserRolesAsync(
        SocketGuildUser user, 
        IReadOnlyCollection<string> values)
    {
        if (values.Contains(SelectionMenuValues.RemoveAll))
        {
            var rolesForRemove = new List<ulong>()
            {
                discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.DestinyDiscordId,
                discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.ValorantDiscordId,
                discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.GachaDiscordId,
                discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.MarathonDiscordId,
                discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.TarkovDiscordId,
            };

            await user.RemoveRolesAsync(rolesForRemove);
            return;
        }

        await user.AddRolesAsync(values.Select(x => ulong.Parse(x)));
    }
}