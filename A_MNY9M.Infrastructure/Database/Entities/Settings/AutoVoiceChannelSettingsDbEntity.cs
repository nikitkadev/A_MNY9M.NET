namespace A_MNY9M.Infrastructure.Database.Entities.Settings;

public sealed class AutoVoiceChannelSettingsDbEntity
{
    public Guid Id { get; set; }
    public ulong UserDiscordId { get; set; }

    public string DefaultName { get; set; } = "mlk_autovoice";
    public int? UserLimit { get; set; }
    public string? Region { get; set; }
}
