namespace A_MNY9M.Application.Features.System.BotInformation;

public class SystemInformationDto
{
    public string AppName { get; set; } = string.Empty;
    public string Version { get; set;  } = string.Empty;
    public string LastUpdateAt { get; set; } = string.Empty;
    public Author Author { get; set; } = new();
    public Company Company { get; set; } = new();
    public Links Links { get; set; } = new();
}

public class Author
{
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string GitHubLink { get; set; } = string.Empty;
    public string TgLink { get; set; } = string.Empty;
}

public class Company
{
    public string Name { get; set; } = string.Empty;
    public string MembersCount { get; set; } = string.Empty;
}

public class Links
{
    public string RepositoryLink { get; set; } = string.Empty;
    public string AboutCommandsLink { get; set; } = string.Empty;
    public string AboutEventsLink { get; set; } = string.Empty;
}