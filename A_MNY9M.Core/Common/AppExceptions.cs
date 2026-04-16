namespace A_MNY9M.Core.Common;

public abstract class AppException(string message) : Exception(message);

public sealed class GuildChannelNotFoundException(ulong channelDiscordId) 
    : AppException($"Канал с DiscordId {channelDiscordId} не является каналом сервера")
{
    public ulong ChannelDiscordId { get; private set; } = channelDiscordId;
}

public sealed class GuildMemberNotFoundException(ulong guildMemberDiscordId) 
    : AppException($"Участник с DiscordId {guildMemberDiscordId} не найден")
{
    public ulong GuildMemberDiscordId { get; private set; } = guildMemberDiscordId;
}