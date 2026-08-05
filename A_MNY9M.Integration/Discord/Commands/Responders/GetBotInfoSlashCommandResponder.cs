using Microsoft.Extensions.Logging;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Application.Features.System.BotInformation;

namespace A_MNY9M.Integration.Discord.Commands.Responders;

public class GetBotInfoSlashCommandResponder(
    ILogger<GetBotInfoSlashCommandResponder> logger) : IDiscordResponseRenderer<GetBotInfoResult>
{
    public async Task RenderAsync(
        SocketSlashCommand slashCommand,
        GetBotInfoResult result)
    {
        try
        {
            var component = new ComponentBuilderV2()
                .WithContainer(
                    container =>
                    {
                        container.WithTextDisplay(
                            $"Системная информация **{result.AppName}** <:dotnet_icon:1495296204328796292>");

                        container.WithTextDisplay(
                            $"Версия **{result.Version}**\n" +
                            $"Автор **{result.Author}**\n" +
                            $"Компания **{result.Company}**");

                        container.WithTextDisplay(
                            $"Последнее обновление **{result.LastUpdateAt:D}**");

                        container.WithSeparator(SeparatorSpacingSize.Small, isDivider: true);

                        container.WithActionRow(
                            row =>
                            {
                                row.WithButton(
                                    async button =>
                                    {
                                        button.WithUrl(result.RepositoryLink.AbsoluteUri);
                                        button.WithLabel("GitHub");
                                        button.WithStyle(ButtonStyle.Link);
                                    });

                                row.WithButton(
                                     button =>
                                     {
                                         button.WithUrl(result.AboutCommandsLink.AbsoluteUri);
                                         button.WithLabel("Команды");
                                         button.WithStyle(ButtonStyle.Link);
                                     });

                                row.WithButton(
                                    button =>
                                    {
                                        button.WithUrl(result.AboutEventsLink.AbsoluteUri);
                                        button.WithLabel("События");
                                        button.WithStyle(ButtonStyle.Link);
                                    });
                            });
                    }).Build();

            await slashCommand.FollowupAsync(
                components: component,
                ephemeral: true);
        }
        catch (Exception ex)
        {
            logger.LogCritical(
                ex,
                "Критическая ошибка при попытке отправить ответное сообщение на команду {slashCommandName}",
                slashCommand.CommandName);

            throw;
        }
    }
}
