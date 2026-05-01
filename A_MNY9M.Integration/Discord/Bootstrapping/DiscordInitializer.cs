using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Bootstrapping;

public class DiscordInitializer(
    IDiscordEventBinder eventBinder) : IDiscordInitializer
{
    public void InitializeAsync()
    {
        eventBinder.Bind();
    }
}
