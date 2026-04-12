namespace A_MNY9M._1_Core.Extensions;

public static class TimeExtension
{
    public static string ConvertTimeFromSecond(long seconds)
    {
        var ts = TimeSpan.FromSeconds(seconds);
        return $"{ts.Days}d {ts.Hours}h {ts.Minutes}m {ts.Seconds}s";
    }
}
