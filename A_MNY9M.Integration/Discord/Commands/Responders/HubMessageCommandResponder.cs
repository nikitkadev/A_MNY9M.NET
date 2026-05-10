using Microsoft.Extensions.Options;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;

namespace A_MNY9M.Integration.Discord.Commands.Responders;

public class HubMessageCommandResponder(
    IOptions<DiscordOption> discordAppConfig,
    IDiscordClientWrapper clientWrapper) : IDiscordResponseRenderer<SendHubMessageResult>
{
    public async Task RenderAsync(
        SocketSlashCommand slashCommand,
        SendHubMessageResult result)
    {
        var jettLoveEmote = await clientWrapper.GetApplicationEmoteAsync(discordAppConfig.Value.AppEmotes.JettLoveIconDiscordId);
        var sageLoveEmote = await clientWrapper.GetApplicationEmoteAsync(discordAppConfig.Value.AppEmotes.SageLoveIconDiscordId);
        var fadeLoveEmote = await clientWrapper.GetApplicationEmoteAsync(discordAppConfig.Value.AppEmotes.FadeLoveIconDiscordId);

        var hubComponent = new ComponentBuilderV2()
            .WithContainer(
                container =>
                {
                    container.WithTextDisplay(result.Title);
                    container.WithTextDisplay(result.Header);
                    container.WithTextDisplay(string.Join("\n", result.Content));
                    container.WithSeparator(SeparatorSpacingSize.Large);

                    container.WithActionRow(
                        row =>
                        {
                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Правила")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(jettLoveEmote)
                                       .WithCustomId(ButtonsCustomIdConsts.Rules);
                                });

                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Роли серввера")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(sageLoveEmote)
                                       .WithCustomId(ButtonsCustomIdConsts.Roles);
                                });

                            row.WithButton(
                                button =>
                                {
                                    button.WithLabel("Цвет имени")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(fadeLoveEmote)
                                       .WithCustomId(ButtonsCustomIdConsts.Colors);
                                });
                        });
                })

        .Build();

        await slashCommand.FollowupAsync(
            components: hubComponent, 
            ephemeral: true);
    }
}