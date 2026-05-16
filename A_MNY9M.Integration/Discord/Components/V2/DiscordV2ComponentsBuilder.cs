using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.AnchorMessages;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.Components.V2;

public class DiscordV2ComponentsBuilder(
    IOptions<AnchorMessagesContent> anchorMessages,
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper,
    IDiscordSelectionMenusBuilder selectionMenusBuilder) : IDiscordV2ComponentsBuilder
{
    public async Task<MessageComponent> BuildHubMessageComponentAsync()
    {
        var rulesEmote = await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub[EmoteNameFromAppSettingsConsts.Rules]);
        var gameRolesEmote = await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub[EmoteNameFromAppSettingsConsts.Roles]);
        var switchNameColorsEmote = await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub[EmoteNameFromAppSettingsConsts.NameColors]);

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
                                async button =>
                                {
                                    button.WithLabel("Роли")
                                        .WithStyle(ButtonStyle.Secondary)
                                        .WithEmote(gameRolesEmote)
                                        .WithCustomId(ButtonsCustomIdConsts.Roles);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Правила")
                                        .WithStyle(ButtonStyle.Secondary)
                                        .WithEmote(rulesEmote)
                                        .WithCustomId(ButtonsCustomIdConsts.Rules);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Цвет имени")
                                        .WithStyle(ButtonStyle.Secondary)
                                        .WithEmote(switchNameColorsEmote)
                                        .WithCustomId(ButtonsCustomIdConsts.Colors);
                                });
                        });
                })

        .Build();
    }

    public async Task<MessageComponent> BuildRulesMessageComponentAsync()
    {
        var rulesContent = string.Empty;

        foreach (var rule in anchorMessages.Value.Rules.Content)
        {
            rulesContent += $"{MarkdownEmote.Dot} {rule}\n";
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
        var menu = await selectionMenusBuilder.GetRolesSetterMenuBuilderAsync();

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.Roles.Title);
                    container.WithTextDisplay(anchorMessages.Value.Roles.Header);
                    container.WithTextDisplay(string.Join("\n", anchorMessages.Value.Roles.Content));

                    container.WithSeparator(SeparatorSpacingSize.Large);

                    container.WithActionRow(
                        row =>
                        {
                            row.WithSelectMenu(menu);
                        });
                })
            .Build();
    }

    public async Task<MessageComponent> BuildColorSwitcherMessageComponentAsync()
    {
        var menu = await selectionMenusBuilder.GetColorSwitcherMenuBuilderAsync();

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.ColorSwitch.Title);
                    container.WithTextDisplay(anchorMessages.Value.ColorSwitch.Header);
                    container.WithTextDisplay(string.Join("\n", anchorMessages.Value.ColorSwitch.Content));
                    container.WithActionRow(
                        row =>
                        {
                            row.WithSelectMenu(menu);
                        });
                })
            .Build();
    }

    public MessageComponent BuildDefaultMessageComponent(string text)
    {
        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(text);
                })
            .Build();
    }
}