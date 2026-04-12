using A_MNY9M._1_Core.Entities;
using Amnyam.Shared.Dtos;

namespace A_MNY9M._2_Application.Interfaces.Services;

public interface IAnalysisService
{
    Task<GuildMemberAIAnalysisResultDto?> GetGuildMemberAIAnalysisAsync(ulong guildMemberDiscordId);
}
