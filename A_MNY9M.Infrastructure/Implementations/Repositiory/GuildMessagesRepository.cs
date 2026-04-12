using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._3_Infrastructure.Database.EF;
using A_MNY9M.Core.Entities.Guild.Channel.Text;

namespace A_MNY9M._3_Infrastructure.Implementations.Repositiory;

public class GuildMessagesRepository(
    ILogger<GuildMessagesRepository> logger,
    AmnyamDbContext mlkAdminDbContext) : IGuildMessagesRepository
{
    public async Task AddMessageAsync(MessageHistory message, CancellationToken token)
    {
        if (message is null)
        {
            logger.LogWarning(
                "Переданная сущность message является null");

            return;
        }

        await mlkAdminDbContext.GuildMessages.AddAsync(message, token);
        await mlkAdminDbContext.SaveChangesAsync(token);

        logger.LogInformation(
            "Сообщение {messageId} успешно записано в базу данных",
            message.MessageDiscordId);
		
    }

    public async Task<IReadOnlyCollection<string?>> GetMessagesColectionByMemberAsync(ulong guildMemberDiscordId)
    {
        var collection = await mlkAdminDbContext.GuildMessages
            .Where(msg => msg.SenderDiscordId == guildMemberDiscordId && msg.Content != string.Empty)
            .OrderBy(msg => msg.SentAt)
            .Select(msg => msg.Content)
            .Take(100)
            .ToListAsync();

        return collection.AsReadOnly();
    }
}
