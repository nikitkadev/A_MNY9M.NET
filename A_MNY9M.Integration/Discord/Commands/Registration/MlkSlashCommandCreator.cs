using Microsoft.Extensions.Logging;

using Discord;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Commands.Registration;

public class MlkSlashCommandCreator(
    ILogger<MlkSlashCommandCreator> logger,
    IDiscordClientWrapper discordClientWrapper) : IDiscordSlashCommandCreator
{
    public async Task CreateGuildSlashCommandAsync()
    {
        try
        {
            var commands = new ApplicationCommandProperties[]
            {
                 new SlashCommandBuilder()
                    .WithName(CommandNameConsts.BotInfoCommandName)
                    .WithDescription("Сервисная информация по приложению A_MNY9M")
                    .Build(),
            };

            await discordClientWrapper.MlkGuild.BulkOverwriteApplicationCommandAsync(commands);
        }
        catch(Exception ex)
        {
            logger.LogCritical(
                ex,
                "Критическая ошибка при попытке переписать команды сервера");

            throw;
        }
    }
}