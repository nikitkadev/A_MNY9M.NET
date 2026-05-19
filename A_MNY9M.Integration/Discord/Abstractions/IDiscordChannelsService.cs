using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordChannelsService
{
    bool IsTemporaryVoiceChannel(ulong channelId);
    Task ClearTemporaryVoiceChannelAsync(ulong channelId);
    Task CreateTemporaryVoiceChannelAsync(SocketGuildUser leader, ulong? categoryId);
}