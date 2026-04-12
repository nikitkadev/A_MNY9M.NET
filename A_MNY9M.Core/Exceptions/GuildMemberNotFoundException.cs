namespace A_MNY9M._1_Core.Exceptions;

public sealed class GuildMemberNotFoundException(ulong guildMemberDiscordId) : DomainException($"Участник с DiscordId {guildMemberDiscordId} не найден")
{
    public ulong GuildMemberDiscordId { get; private set; } = guildMemberDiscordId;
}
