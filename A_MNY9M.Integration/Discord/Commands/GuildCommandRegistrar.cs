using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Commands.Builders;

namespace A_MNY9M.Integration.Discord.Commands;

public sealed class GuildCommandRegistrar(
    DiscordSocketClient discordSocketClient)
{
    public async Task RegisterAsync(ulong guildId)
    {
        var guild = discordSocketClient.GetGuild(guildId)
            ?? throw new InvalidOperationException("Сервер не найден.");

        await guild.DeleteApplicationCommandsAsync();

        await guild.CreateApplicationCommandAsync(SetSourceVoiceChannelCommandBuilder.Build());
        await guild.CreateApplicationCommandAsync(SetAutoLobbySettingsCommandBuild.Build());
    }
}
