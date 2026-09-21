namespace Day09_Result.Data.Models;

public sealed record Subway(
    string LineId,
    string Direction,
    string TrainLineName,
    string Destination,
    string ArrivalMessage,
    string ArrivalMessageDetail,
    string ReceivedAt);