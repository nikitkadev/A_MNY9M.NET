using A_MNY9M.Application.Features.System.AnchorMessages;

namespace A_MNY9M.Application.Abstrations;

public interface IAnchorMessageProvider 
{
    AnchorMessagesContent AnchorMessages { get; }
}
