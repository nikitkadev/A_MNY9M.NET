using A_MNY9M._3_Infrastructure.Providers.Models.Guild;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Guild;

public interface IJsonGuildCategoriesProvider
{
    ConfigCategory Admin { get; }
    ConfigCategory Server { get; }
    ConfigCategory Base { get; }
    ConfigCategory Game { get; }
    ConfigCategory Lobby{ get; }
    ConfigCategory Rest{ get; }
}