using MediatR;

using A_MNY9M.Application.Abstrations;

namespace A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;

public class SendHubMessageCommandHandler(
    IAnchorMessageProvider anchorMessageProvider) : IRequestHandler<SendHubMessageCommand, SendHubMessageResult>
{
    public Task<SendHubMessageResult> Handle(
        SendHubMessageCommand request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            new SendHubMessageResult(
                Title: anchorMessageProvider.AnchorMessages.Hub.Title,
                Header: anchorMessageProvider.AnchorMessages.Hub.Header,
                Content: anchorMessageProvider.AnchorMessages.Hub.Content,
                InviteLink: anchorMessageProvider.AnchorMessages.Hub.Additional.InviteLink,
                Footer: anchorMessageProvider.AnchorMessages.Hub.Footer));
    }
}