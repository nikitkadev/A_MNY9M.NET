using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Components.SelectionMenus;

public class DiscordSelectionMenusBuilder(
    IOptions<DiscordOption> discordAppOptions,
    IDiscordClientWrapper discordClientWrapper) : IDiscordSelectionMenusBuilder
{
    public async Task<SelectMenuBuilder> RolesSelectionMenu()
    {
        var valorantIconEmote = await discordClientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.ValorantIconDiscordId);
        var marathonIconEmote = await discordClientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.GenshinPullDiscordId);
        var destinyIconEmote = await discordClientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.ValorantIconDiscordId);
        var tarkovIconEmote = await discordClientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.GenshinPullDiscordId);
        var gachaIconEmote = await discordClientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.GenshinPullDiscordId);

        return new SelectMenuBuilder()
            .WithCustomId(SelectionMenuIdConsts.RolesMenu)

            .WithPlaceholder("Выберите игровые роли")
            .WithOptions([

                new SelectMenuOptionBuilder()
                    .WithLabel("очиᴄᴛиᴛь ʙᴄᴇ")
                    .WithValue(SelectionMenuValues.RemoveAll),

                new SelectMenuOptionBuilder()
                    .WithLabel("Valorant")
                    .WithValue(discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.ValorantDiscordId.ToString())
                    .WithEmote(valorantIconEmote),

                new SelectMenuOptionBuilder()
                    .WithLabel("Marathon")
                    .WithValue(discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.MarathonDiscordId.ToString())
                    .WithEmote(marathonIconEmote),

                new SelectMenuOptionBuilder()
                    .WithLabel("Destiny 2")
                    .WithValue(discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.DestinyDiscordId.ToString())
                    .WithEmote(destinyIconEmote),

                new SelectMenuOptionBuilder()
                    .WithLabel("Tarkov")
                    .WithValue(discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.TarkovDiscordId.ToString())
                    .WithEmote(tarkovIconEmote),

                new SelectMenuOptionBuilder()
                    .WithLabel("Gacha Depper")
                    .WithValue(discordAppOptions.Value.MalenkieGuild.TestingSelectedRoles.GachaDiscordId.ToString())
                    .WithEmote(gachaIconEmote),

            ])
            .WithMinValues(1)
            .WithMaxValues(6);
    }
}