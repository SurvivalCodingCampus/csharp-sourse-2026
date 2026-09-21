using Day09_Result.Data.DTOs;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

public static class SubwayMapper
{
    public static Subway ToModel(this SubwayArrivalDto dto) => new(
        LineId: dto.SubwayId ?? string.Empty,
        Direction: dto.UpDownLine ?? string.Empty,
        TrainLineName: dto.TrainLineName ?? string.Empty,
        Destination: dto.Destination ?? string.Empty,
        ArrivalMessage: dto.ArrivalMessage ?? string.Empty,
        ArrivalMessageDetail: dto.ArrivalMessageDetail ?? string.Empty,
        ReceivedAt: dto.ReceivedAt ?? string.Empty);
}