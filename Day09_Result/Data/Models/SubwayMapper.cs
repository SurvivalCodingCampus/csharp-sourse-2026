using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.Models;

public static class SubwayMapper
{
    public static SubwayArrivalInfo ToDomain(this RealtimeArrivalItemDto dto)
    {
        int.TryParse(dto.BarvlDt, out int seconds);

        return new SubwayArrivalInfo
        {
            StationName = string.IsNullOrWhiteSpace(dto.StatnNm) ? "미확인역" : dto.StatnNm.Trim(),
            DestinationLine = string.IsNullOrWhiteSpace(dto.TrainLineNm) ? "방면 정보 없음" : dto.TrainLineNm.Trim(),
            ArrivalMessage = string.IsNullOrWhiteSpace(dto.ArvlMsg2) ? "도착 정보 없음" : dto.ArvlMsg2.Trim(),
            RemainingSeconds = seconds,
            CurrentLocation = string.IsNullOrWhiteSpace(dto.ArvlMsg3) ? "-" : dto.ArvlMsg3.Trim()
        };
    }
}