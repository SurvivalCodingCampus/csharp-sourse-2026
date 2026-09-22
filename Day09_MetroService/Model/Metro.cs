namespace Day09_MetroService.Model;
public sealed record Metro(
    string StationName, string LineName, string Direction, string Destination,
    string TrainNumber, string TrainType, int? ArrivalSeconds,
    string ArrivalMessage, string LocationMessage, DateTimeOffset? ReceivedAt);
