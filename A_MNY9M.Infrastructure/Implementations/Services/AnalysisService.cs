using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Amnyam.Shared.Dtos;
using A_MNY9M._2_Application.Interfaces.Services;
using A_MNY9M._3_Infrastructure.Interfaces.ChatGPT;
using A_MNY9M.Core.Interfaces.Repository;

namespace A_MNY9M._3_Infrastructure.Implementations.Services;

public class AnalysisService(ILogger<AnalysisService> logger,
    IGuildMessagesRepository messagesRepository,
    IChatGPTService chatService) : IAnalysisService
{
    public async Task<GuildMemberAIAnalysisResultDto?> GetGuildMemberAIAnalysisAsync(ulong guildMemberDiscordId)
    {
        try
        {
            var messages = await messagesRepository.GetMessagesColectionByMemberAsync(guildMemberDiscordId);

            if (messages is null || messages.Count == 0)
            {
                logger.LogWarning(
                    "Сообщений от участника с DiscordId {GuildMemberDiscordId} не найдено",
                    guildMemberDiscordId);

                return null;
            }

            var analysisAIResult = await chatService.AnalyzeWithAIGuildMemberAsync(messages);

            return analysisAIResult;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(
                ex,
                "Ошибка при попытке провести AI-анализ для участника с DiscordId {GuildMemberDiscordId}",
                guildMemberDiscordId);

            return null;
        }
    }
}
