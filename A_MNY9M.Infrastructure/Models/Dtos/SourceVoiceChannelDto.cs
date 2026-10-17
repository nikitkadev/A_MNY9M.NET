namespace A_MNY9M.Infrastructure.Models.Dtos;

public sealed record SourceVoiceChannelDto(
    Guid Id,
    string Name,
    ulong DiscordId,
    ulong InitiatorDiscordId,
    ulong? GuildCategoryId,
    DateTime? SetAt);
