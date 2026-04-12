using A_MNY9M._1_Core.Entities;
using Amnyam.Shared.Dtos;
using Amnyam.Shared.Results;

namespace A_MNY9M._2_Application.Interfaces.Managers;

public interface IGuildMembersManager
{
    Task<BaseResult> AuthorizeGuildMemberAsync(ulong guildMemberDiscordId, string guildMemberMention);
    Task<BaseResult> DeauthorizeGuildMemberAsync(ulong guildMemberDiscordId, string guildMemberGlobalName);
    Task<BaseResult> UpdateGuildMemberColorRoleAsync(ulong guildMemberDiscordId, string guildRoleKey);
    Task<BaseResult<GuildMemberAnalysisResultData>> AnalyzeGuildMemberAsync(ulong guildMemberDiscordId);
    Task<BaseResult> WelcomeNewMemberAsync(GuildMember guildMemberEntity);
}
