using Discord;

namespace A_MNY9M.Integration.Discord.Components.V2;

public static class RulesV2ComponentBuilder
{
    public static MessageComponent Build(IEmote dotEmote)
    {
        var messageComponent = new ComponentBuilderV2()
            .WithContainer(container => {
                container.WithTextDisplay("");
            })
            .Build();

        return messageComponent;
    }
}