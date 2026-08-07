using Discord;

namespace A_MNY9M.Integration.Discord.Components.V2;

public static class DefaultV2ComponentBuilder
{
    public static MessageComponent Build(string message)
    {
        var messageComponent = new ComponentBuilderV2()
            .WithContainer(container =>
            {
                container.WithTextDisplay(message);
            });

        return messageComponent.Build();
    }
}