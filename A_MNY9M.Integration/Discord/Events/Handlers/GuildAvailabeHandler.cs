using Microsoft.Extensions.Logging;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Commands;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class GuildAvailabeHandler(
    ILogger<GuildAvailabeHandler> logger,
    GuildCommandRegistrar guildCommandRegistrar)
{
    public async Task HandleAsync(SocketGuild guild)
    {
        await guildCommandRegistrar.RegisterAsync(guild.Id);

        logger.LogInformation("Команды сервера успешно зарегистрированы.");
    }
}
