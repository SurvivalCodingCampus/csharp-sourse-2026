namespace Day09_Result.Data.Models;

public class SubwayArrivalInfo
{
    public string StationName { get; init; } = string.Empty;
    public string DestinationLine { get; init; } = string.Empty;
    public string ArrivalMessage { get; init; } = string.Empty;
    public int RemainingSeconds { get; init; }
    public string CurrentLocation { get; init; } = string.Empty;
}