using Microsoft.Extensions.Options;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;

namespace A_MNY9M.Integration.Discord.Commands.Responders;

public class HubMessageCommandResponder(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordResponseRenderer<SendHubMessageResult>
{
    public async Task RenderAsync(
        SocketSlashCommand slashCommand,
        SendHubMessageResult result)
    {
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
                                async button =>
                                {
                                    button.WithLabel("Правила")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub["PixelPinkHeartDiscordId"]))
                                       .WithCustomId(ButtonsCustomIdConsts.Rules);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Роли")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub["PixelPinkHeartsDiscordId"]))
                                       .WithCustomId(ButtonsCustomIdConsts.Roles);
                                });

                            row.WithButton(
                                async button =>
                                {
                                    button.WithLabel("Цвет имени")
                                       .WithStyle(ButtonStyle.Secondary)
                                       .WithEmote(await clientWrapper.GetApplicationEmoteAsync(malenkieOptions.Value.Emotes.ForGuildHub["PixelRedHeartDiscordId"]))
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