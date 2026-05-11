using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Application.Features.System.AnchorMessages;
using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.Components.V2;

public class V2Builder(
    IOptions<AnchorMessagesContent> anchorMessages,
    IOptions<DiscordOption> discordAppOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordComponentsBuilder
{
    public async Task<MessageComponent> BuildWelcomeMessageComponentsAsync()
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
                                    button.WithLabel("Роли")
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
}