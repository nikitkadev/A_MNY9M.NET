using A_MNY9M._3_Infrastructure.Providers.Models.App;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.App;

public interface IJsonMlkAdminAppConfigurationProvider
{
    string MalenkieApiKey { get; }
    string OpenAIApiKey { get; }
    string ConnectionString { get; }
    BotDetails BotDetails { get; }
}
