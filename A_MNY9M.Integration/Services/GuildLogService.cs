using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Content.Models;
using Microsoft.Extensions.Logging;

namespace A_MNY9M.Integration.Services;

public sealed class GuildLogService(
    ILogger<GuildLogService> logger,
    IOptions<GuildConfigurationOption> guildOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task SendLogAsync(string message)
    {

        logger.LogInformation(
            "Переданный guildDiscordId: {guildId}", 
            guildOption.Value.DiscordId);

        var guild = discordSocketClient.GetGuild(guildOption.Value.TestDiscordId);

        if(guild is null)
        {
            logger.LogInformation(
                $"Гильдия не найдена.");

            return;
        }

        var channel = guild.GetTextChannel(1535879903076024463);

        await channel.SendMessageAsync(message);
    }
}
