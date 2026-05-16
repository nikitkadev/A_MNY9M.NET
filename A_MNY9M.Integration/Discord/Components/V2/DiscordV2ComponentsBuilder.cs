using A_MNY9M.Application.Features.System.AnchorMessages;
using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;
using Discord;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

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
                                    button.WithLabel("Роли");
                                    button.WithStyle(ButtonStyle.Secondary);
                                    button.WithEmote(gameRolesEmote);
                                    button.WithCustomId(ButtonsCustomIdConsts.Roles);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Правила");
                                    button.WithStyle(ButtonStyle.Secondary);
                                    button.WithEmote(rulesEmote);
                                    button.WithCustomId(ButtonsCustomIdConsts.Rules);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Цвет имени");
                                    button.WithStyle(ButtonStyle.Secondary);
                                    button.WithEmote(switchNameColorsEmote);
                                    button.WithCustomId(ButtonsCustomIdConsts.Colors);
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

                    container.WithSeparator(SeparatorSpacingSize.Large);

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

    public async Task<MessageComponent> BuildWelcomeMessageComponent(string userMention)
    {
        var chocolaSmugEmote = await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForWelcome["Chocolasmug"]);

        return new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(anchorMessages.Value.Joined.Title);
                    container.WithTextDisplay(anchorMessages.Value.Joined.Header.Replace("{user.Mention}", userMention));

                    container.WithSeparator(SeparatorSpacingSize.Small);

                    container.WithActionRow(
                        row =>
                        {
                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Предсказание");
                                    button.WithStyle(ButtonStyle.Secondary);
                                    button.WithEmote(chocolaSmugEmote);
                                    button.WithCustomId(ButtonsCustomIdConsts.Predict);

                                });

                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Центр Malenkie");
                                    button.WithStyle(ButtonStyle.Link);
                                    button.WithUrl(anchorMessages.Value.Joined.Additional.HubLink);
                                });
                        });
                })
            .Build();
    }
}