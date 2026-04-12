using Amnyam.Shared.JsonProviders;

namespace A_MNY9M._3_Infrastructure.Providers.Interfaces.Configuration.Messages;

public interface IWelcomeMessageProvider : IJsonMessageContentProvider<string>;
public interface IRulesMessageProvider : IJsonMessageContentProvider<string>;
public interface IColorMessageProvider : IJsonMessageContentProvider<string>;
public interface IHubMessageProvider : IJsonMessageContentProvider<string>;
