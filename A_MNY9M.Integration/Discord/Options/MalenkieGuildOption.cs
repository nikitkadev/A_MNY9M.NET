namespace A_MNY9M.Integration.Discord.Options;

public class MalenkieGuildOption
{
    public ulong DiscordId { get; set; }
    public string Name { get; set; } = string.Empty;

    public SelectedRolesDictionaries TestSelectedCategoryRoles { get; set; } = new();
    public SelectedRolesDictionaries TestSelectedColorRoles { get; set; } = new();
    public AnchorMessages AnchorMessages { get; set; } = new();
    public Emotes Emotes { get; set; } = new();
}

public class SelectedRolesDictionaries
{
    public Dictionary<string, ulong> RoleIds { get; set; } = [];
    public Dictionary<string, ulong> EmoteIds { get; set; } = [];
}

public class AnchorMessages
{
    public Dictionary<string, ulong> ChannelsIn { get; set; } = [];
    public Dictionary<string, ulong> Ids { get; set; } = [];
}

public class Emotes
{
    public Dictionary<string, ulong> ForGuildHub { get; set; } = [];
}