namespace A_MNY9M.Application.Features.System.AnchorMessages;

public class AnchorMessagesContent
{
    public MessageContent Hub { get; set; } = new();
    public MessageContent Rule { get; set; } = new();
}

public class MessageContent
{
    public string Title { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public List<string> Content { get; set; } = [];
    public Additional Additional { get; set; } = new();
    public string Footer { get; set; } = string.Empty;
}

public class Additional
{
    public string InviteLink { get; set; } = string.Empty;
}