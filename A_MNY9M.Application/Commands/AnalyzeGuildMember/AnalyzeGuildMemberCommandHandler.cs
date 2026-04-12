using MediatR;

using A_MNY9M._1_Core.Dtos;
using A_MNY9M._1_Core.Results;

using A_MNY9M._2_Application.Interfaces.Managers;

namespace A_MNY9M._2_Application.Commands.AnalyzeGuildMember;

public class AnalyzeGuildMemberCommandHandler(
    IGuildMembersManager membersManager) : IRequestHandler<AnalyzeGuildMemberCommand, BaseResult<GuildMemberAnalysisResultData>>
{
    public async Task<BaseResult<GuildMemberAnalysisResultData>> Handle(AnalyzeGuildMemberCommand request, CancellationToken cancellationToken)
    {
        return await membersManager.AnalyzeGuildMemberAsync(request.GuildMemberDiscordId);
    }
}
