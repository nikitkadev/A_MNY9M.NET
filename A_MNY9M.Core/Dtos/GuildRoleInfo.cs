using A_MNY9M._1_Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace A_MNY9M._1_Core.Dtos;

public class GuildRoleInfo
{
    public ulong Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public RoleType Type { get; set; }
}
