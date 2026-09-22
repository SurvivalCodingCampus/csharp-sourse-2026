using Day09_Result_Pattern.Data.DTO;
using Day09_Result_Pattern.Data.Models;

namespace Day09_Result_Pattern.Data.Mapper;

public static class SubwayMapper
{
    public static Subway ToModel(
        this SubwayArrivalDto dto)
    {
        int.TryParse(
            dto.ArrivalSeconds,
            out var arrivalSeconds
        );

        if (arrivalSeconds < 0)
        {
            arrivalSeconds = 0;
        }

        return new Subway(
            arrivalSeconds,
            dto.ArrivalMessage ?? "정보 없음",
            dto.CurrentLocation ?? "정보 없음",
            dto.ArrivalCode ?? "정보 없음",
            dto.Direction ?? "정보 없음",
            dto.TrainLineName ?? "정보 없음",
            dto.TerminalStation ?? "정보 없음",
            dto.StationName ?? "Unknown"
        );
    }
}