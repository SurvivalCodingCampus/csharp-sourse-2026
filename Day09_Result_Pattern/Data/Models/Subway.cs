namespace Day09_Result_Pattern.Data.Models;

public class Subway
{
    public int ArrivalSeconds { get; }
    public string ArrivalMessage { get; }
    public string CurrentLocation { get; }
    public string ArrivalCode { get; }
    public string Direction { get; }
    public string TrainLineName { get; }
    public string TerminalStation { get; }
    public string StationName { get; }

    public Subway(
        int arrivalSeconds,
        string arrivalMessage,
        string currentLocation,
        string arrivalCode,
        string direction,
        string trainLineName,
        string terminalStation,
        string stationName)
    {
        ArrivalSeconds = arrivalSeconds;
        ArrivalMessage = arrivalMessage;
        CurrentLocation = currentLocation;
        ArrivalCode = arrivalCode;
        Direction = direction;
        TrainLineName = trainLineName;
        TerminalStation = terminalStation;
        StationName = stationName;
    }

    public override string ToString()
    {
        return
            $"역 이름: {StationName}\n" +
            $"방향: {Direction}\n" +
            $"열차 정보: {TrainLineName}\n" +
            $"종착역: {TerminalStation}\n" +
            $"도착 정보: {ArrivalMessage}\n" +
            $"현재 위치: {CurrentLocation}\n" +
            $"도착까지 남은 시간: {ArrivalSeconds}초\n" +
            $"도착 상태 코드: {ArrivalCode}";
    }
}