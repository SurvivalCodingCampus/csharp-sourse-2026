using Day09_Result.Data.DTOs;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

public static class SubwayArrivalMapper
{
    public static SubwayArrival ToSubwayArrival(this SubwayArrivalResponseDto dto)
    {
        return new SubwayArrival(
            dto.ErrorMessage?.Status ?? -1,
            dto.RealtimeArrivalList?.FirstOrDefault()?.StatnNm ?? "역이름 없음",
            dto.RealtimeArrivalList?.FirstOrDefault()?.BstatnNm ?? "종착지 없음",
            dto.RealtimeArrivalList?.FirstOrDefault()?.RecptnDt ?? "도착시간 없음");
    }
}