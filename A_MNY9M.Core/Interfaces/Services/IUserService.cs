namespace A_MNY9M.Core.Interfaces.Services;

public interface IUserService
{
    Task ExecuteJoinPipelineAsync(ulong userId, string userMention = "");
}
