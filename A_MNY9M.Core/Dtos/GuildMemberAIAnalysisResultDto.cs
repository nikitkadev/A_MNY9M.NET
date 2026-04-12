namespace A_MNY9M._1_Core.Dtos;

public class GuildMemberAIAnalysisResultDto
{
    public float AvgToxicity { get; set; }
    public string MostToxicMessage { get; set; } = string.Empty;
    public string SpeechStyle { get; set; } = string.Empty;
    public string Tonality { get; set; } = string.Empty;
    public float AvgCharsInMessage { get; set; }
}
