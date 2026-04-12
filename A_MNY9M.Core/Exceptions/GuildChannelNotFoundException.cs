namespace A_MNY9M._1_Core.Exceptions;

public sealed class GuildChannelNotFoundException(ulong channelDiscordId) : DomainException($"Канал с DiscordId {channelDiscordId} не является каналом сервера")
{
    public ulong ChannelDiscordId { get; private set; } = channelDiscordId;
}
