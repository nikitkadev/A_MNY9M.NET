using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._3_Infrastructure.Database.EF;
using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M._3_Infrastructure.Implementations.Repositiory;

public class GuildVoiceSessionRepository(
    AmnyamDbContext dbContext) : IGuildVoiceSessionRepository
{
    public async Task AddGuildVoiceSessionAsync(VoiceSession session)
    {
        await dbContext.GuildVoiceSessions.AddAsync(session);
        await dbContext.SaveChangesAsync(); 
    }
}
