namespace A_MNY9M.Infrastructure.Options.Models;

public sealed class DatabaseOption
{
    public const string SectionName = "Database";

    public string LocalConnectionString { get; set; } = string.Empty;
    public int CommandTimeoutInSeconds { get; set; }
}
