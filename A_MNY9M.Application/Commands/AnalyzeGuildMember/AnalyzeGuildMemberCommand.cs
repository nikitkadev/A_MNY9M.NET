using MediatR;

using A_MNY9M._1_Core.Dtos;
using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.AnalyzeGuildMember;

public class AnalyzeGuildMemberCommand : IRequest<BaseResult<GuildMemberAnalysisResultData>>
{
    public ulong GuildMemberDiscordId { get; set; }
}
