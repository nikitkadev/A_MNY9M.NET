namespace A_MNY9M.Infrastructure.Models.Dtos;

public sealed record AutoVoiceChannelSettingsDto(
    string Name,
    int? UserLimit,
    string? Region);
