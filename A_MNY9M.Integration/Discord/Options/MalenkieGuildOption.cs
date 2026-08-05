using System.Security;

namespace A_MNY9M.Integration.Discord.Options;

public class MalenkieGuildOption
{
    public ulong DiscordId { get; set; }
    public string Name { get; set; } = string.Empty;

    public SelectedRolesDictionaries TestSelectedCategoryRoles { get; set; } = new();
    public SelectedRolesDictionaries TestSelectedColorRoles { get; set; } = new();
    public AnchorMessages AnchorMessages { get; set; } = new();
    public Emotes Emotes { get; set; } = new();
    public GuildRoles GuildRoles { get; set; } = new();
    public Channels Channels { get; set; } = new();

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
    public Dictionary<string, ulong> ForWelcome { get; set; } = [];
    public Dictionary<string, ulong> ForStatistic { get; set; } = [];
}

public class GuildRoles
{
    public Dictionary<string, ulong> Hierarchy { get; set; } = [];
}

public class Channels
{
    public Dictionary<string, ulong> Text { get; set; } = [];
    public Voice Voice { get; set; } = new();

}

public class Voice
{
    public Dictionary<string, ulong> SessionSpawners { get; set; } = [];
}