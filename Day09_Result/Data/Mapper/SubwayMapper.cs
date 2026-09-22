using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

public static class SubwayMapper
{
    public static List<Subway> ToModel(this List<RealtimeArrivalDto>? dtos) 
    {
        if (dtos == null)
        {
            return new List<Subway>();
        }

        return dtos.Select(dto => new Subway(
            StatnNm: dto.StatnNm ?? "정보 없음",
            UpdnLine: dto.UpdnLine ?? "정보 없음",
            TrainLineNm: dto.TrainLineNm ?? "정보 없음",
            RecptnDt: dto.RecptnDt ?? "정보 없음",
            ArvlMsg2: dto.ArvlMsg2 ?? "정보 없음"
        )).ToList();
    }
}