namespace A_MNY9M.Integration.Models.Params;

public sealed record SetSourceVoiceChannelParams(
    ulong DiscordId,
    string Name,
    ulong? DiscordCategoryId,
    ulong InitiatorDiscordId);
