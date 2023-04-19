namespace blazor_tailwind_netflix.Client.Extensions;

public static class TimeSpanExtensions
{
    public static string FormatDurationToMinutes(this TimeSpan duration) =>  $"{duration.TotalMinutes.ToString():0} minutes";
}