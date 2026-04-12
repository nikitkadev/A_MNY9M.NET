using MediatR;

using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.Test;

public class TestCommand : IRequest<BaseResult>
{
    public string MemberPrompt { get; set; } = string.Empty;
}
