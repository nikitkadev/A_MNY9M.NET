using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Integration.Constants;
using A_MNY9M.Integration.Discord.Content.Models;

namespace A_MNY9M.Integration.Services;

public sealed class GuildLogService(
    ILogger<GuildLogService> logger,
    IOptions<GuildConfigurationOption> guildOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task TrySendLogAsync(string message)
    {
        try
        {
            var guild = discordSocketClient.GetGuild(
                guildOption.Value.TestDiscordId);

            if (guild is null)
            {
                logger.LogWarning(
                    "Гильдия {GuildId} не найдена.",
                    guildOption.Value.TestDiscordId);

                return;
            }

            if (!guildOption.Value.Channels.TryGetValue(
                DiscordChannelCodes.BotLogs,
                out var adminChannelDiscordId))
            {
                logger.LogWarning(
                    "Не настроен канал логирования с кодом {ChannelCode}.",
                    DiscordChannelCodes.BotLogs);

                return;
            }

            var channel = guild.GetTextChannel(
                adminChannelDiscordId);

            if (channel is null)
            {
                logger.LogWarning(
                    "Канал с переданным id {channelId} не был найден на сервере.",
                    adminChannelDiscordId);

                return;
            }

            await channel.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка отправки Discord лог-сообщения.");
        }

    }
}
