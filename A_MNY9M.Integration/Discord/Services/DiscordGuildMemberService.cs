using Microsoft.Extensions.Options;

using A_MNY9M.Core.Interfaces.Services;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Services;

public class DiscordGuildMemberService(
    IDiscordMessagesSender discordMessageSender,
    IDiscordV2ComponentsBuilder discordV2ComponentsBuilder,
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordRolesManager rolesManager) : IUserService
{
    public async Task ExecuteJoinPipelineAsync(
        ulong userId, 
        string userMention = "")
    {
        await rolesManager.SetRolesToUserAsync(
           userId: userId,
           roleIds: [malenkieOptions.Value.GuildRoles.Hierarchy["Member"]]);

        var welcomeComponent = await discordV2ComponentsBuilder.BuildWelcomeMessageComponent(userMention);

        await discordMessageSender.SendWelcomeMessageToNewMemberAsync(
            messageComponent: welcomeComponent);
    }
}