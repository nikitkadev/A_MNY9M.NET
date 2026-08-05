using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Components.SelectionMenus;

public class DiscordSelectionMenusBuilder(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordSelectionMenusBuilder
{
    public async Task<SelectMenuBuilder> GetRolesSetterMenuBuilderAsync()
    {
        return new SelectMenuBuilder()
            .WithCustomId(SelectionMenuIdConsts.RolesMenu)
            .WithPlaceholder("Выберите игровые роли")
            .WithMinValues(1)
            .WithMaxValues(6)
            .WithOptions([

                new SelectMenuOptionBuilder()
                    .WithLabel("Destiny 2")
                    .WithValue(malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds["DestinyDiscordId"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedCategoryRoles.EmoteIds["DestinyDiscordId"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Gacha Depper")
                    .WithValue(malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds["GachaDiscordId"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedCategoryRoles.EmoteIds["GachaDiscordId"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Valorant")
                    .WithValue(malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds["ValorantDiscordId"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedCategoryRoles.EmoteIds["ValorantDiscordId"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Marathon")
                    .WithValue(malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds["MarathonDiscordId"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedCategoryRoles.EmoteIds["MarathonDiscordId"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Tarkov")
                    .WithValue(malenkieOptions.Value.TestSelectedCategoryRoles.RoleIds["TarkovDiscordId"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedCategoryRoles.EmoteIds["TarkovDiscordId"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("очиᴄᴛиᴛь ʙᴄᴇ")
                    .WithValue(SelectionMenuValues.RemoveAll)
            ]);
    }

    public async Task<SelectMenuBuilder> GetColorSwitcherMenuBuilderAsync()
    {
        return new SelectMenuBuilder()
            .WithCustomId(SelectionMenuIdConsts.ColorsMenu)
            .WithPlaceholder("Выберите самый крутой цвет")
            .WithOptions([
                    
                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Lime")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Green"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Green"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Red")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Red"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Red"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Blue")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Blue"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Blue"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Orange")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Orange"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Orange"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Yellow")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Yellow"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Yellow"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Purple [Nitro]")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Purple"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Purple"])),
                    
                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Pink [Nitro]")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Pink"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Pink"])),

                new SelectMenuOptionBuilder()
                    .WithLabel("Malenkie Silver [Nitro]")
                    .WithValue(malenkieOptions.Value.TestSelectedColorRoles.RoleIds["Silver"].ToString())
                    .WithEmote(await clientWrapper.GetApplicationEmoteAsync(
                        malenkieOptions.Value.TestSelectedColorRoles.EmoteIds["Silver"])),

                 new SelectMenuOptionBuilder()
                    .WithLabel("удᴀᴧиᴛь цʙᴇᴛ")
                    .WithValue(SelectionMenuValues.RemoveAll)
            ]);
    }
}