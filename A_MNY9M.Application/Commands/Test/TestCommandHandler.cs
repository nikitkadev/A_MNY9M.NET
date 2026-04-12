using MediatR;

using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.Test;

public class TestCommandHandler : IRequestHandler<TestCommand, BaseResult>
{
    public async Task<BaseResult> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        return default;
    }
}