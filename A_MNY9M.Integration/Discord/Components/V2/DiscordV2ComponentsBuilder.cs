using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.AnchorMessages;

namespace A_MNY9M.Integration.Discord.Components.V2;

public class DiscordV2ComponentsBuilder(
    IOptions<AnchorMessagesContent> anchorMessages,
    IOptions<DiscordOption> discordAppOptions,
    IDiscordClientWrapper clientWrapper,
    IDiscordSelectionMenusBuilder selectionMenusBuilder) : IDiscordV2ComponentsBuilder
{
    public async Task<MessageComponent> BuildWelcomeMessageComponentAsync()
    {
        var emoteForRules = await clientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.PixelRedStarDiscordId);
        var emoteForRoles = await clientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.PixelOrangeStarDiscordId);
        var emoteForNameColors = await clientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.PixelGreenStarDiscordId);

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.Hub.Title);
                    container.WithTextDisplay(anchorMessages.Value.Hub.Header);
                    container.WithTextDisplay(string.Join("\n", anchorMessages.Value.Hub.Content));
                    container.WithSeparator(SeparatorSpacingSize.Large);

                    container.WithActionRow(
                        row =>
                        {
                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Правила")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(emoteForRules)
                                       .WithCustomId(ButtonsCustomIdConsts.Rules);
                                });

                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Игровые роли")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(emoteForRoles)
                                       .WithCustomId(ButtonsCustomIdConsts.Roles);
                                });

                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Цвет имени")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(emoteForNameColors)
                                       .WithCustomId(ButtonsCustomIdConsts.Colors);
                                });
                        });
                })

        .Build();
    }
    public async Task<MessageComponent> BuildRulesMessageComponentAsync()
    {
        var dotEmote = await clientWrapper.GetApplicationEmoteAsync(discordAppOptions.Value.AppEmotes.Dot);
        var rulesContent = string.Empty;

        foreach(var rule in anchorMessages.Value.Rules.Content)
        {
            rulesContent += $"{dotEmote} {rule}\n";
        }

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.Rules.Title);
                    container.WithTextDisplay(anchorMessages.Value.Rules.Header);
                    container.WithTextDisplay(rulesContent);

                }).Build();
    }
    public async Task<MessageComponent> BuildRolesMessageComponentAsync()
    {
        var menu = await selectionMenusBuilder.RolesSelectionMenu();

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.Roles.Title);
                    container.WithTextDisplay(anchorMessages.Value.Roles.Header);
                    container.WithTextDisplay(string.Join("\n", anchorMessages.Value.Roles.Content));

                    container.WithSeparator(SeparatorSpacingSize.Large);

                    container.WithActionRow(row =>
                    {
                        row.WithSelectMenu(menu);
                    });
                })
            .Build();
    }
}