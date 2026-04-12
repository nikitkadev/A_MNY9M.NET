using Discord.WebSocket;

namespace A_MNY9M._2_Application.Interfaces.Managers;

public interface IGuildVoiceSessionsManager
{
    Task HandleVoiceStateUpdateAsync(SocketVoiceChannel? newVoiceChannel, SocketVoiceChannel? oldVoiceChannel, ulong guildMemberDiscordId);
}
