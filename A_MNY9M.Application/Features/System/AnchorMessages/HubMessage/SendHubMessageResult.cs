namespace A_MNY9M.Application.Features.System.AnchorMessages.HubMessage;

public record SendHubMessageResult(
    string Title,
    string Header,
    List<string> Content,
    string InviteLink,
    string Footer);
