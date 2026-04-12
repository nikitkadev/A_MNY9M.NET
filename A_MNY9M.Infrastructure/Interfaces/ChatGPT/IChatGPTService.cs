using Amnyam.Shared.Dtos;

namespace A_MNY9M._3_Infrastructure.Interfaces.ChatGPT;

public interface IChatGPTService
{
    Task<GuildMemberAIAnalysisResultDto> AnalyzeWithAIGuildMemberAsync(IReadOnlyCollection<string?> guildMemberMessages);
}
