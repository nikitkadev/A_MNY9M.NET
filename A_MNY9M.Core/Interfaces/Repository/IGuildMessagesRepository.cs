using A_MNY9M.Core.Entities.Guild.Channel.Text;

namespace A_MNY9M.Core.Interfaces.Repository;

public interface IGuildMessagesRepository
{
    public Task AddMessageAsync(MessageHistory message, CancellationToken token);
    Task<IReadOnlyCollection<string?>> GetMessagesColectionByMemberAsync(ulong guildMemberDiscordId);

}
