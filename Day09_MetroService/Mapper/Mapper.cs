using System.Globalization;
using Day09_MetroService.DTO;
using Day09_MetroService.Model;
namespace Day09_MetroService.Mapper;
public static class Mapper
{
    public static Metro ToModel(this ArrivalDTO dto)
    {
        int? seconds = int.TryParse(dto.ArrivalSeconds, out var value) && value >= 0 ? value : null;
        DateTimeOffset? received = DateTime.TryParseExact(dto.ReceivedAt,
            "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
            ? new DateTimeOffset(date, TimeSpan.FromHours(9)) : null;
        var line = dto.SubwayId switch
        {
            //case : 을 => 으로 바꿈
            "1001" => "1호선", "1002" => "2호선", "1003" => "3호선",
            "1004" => "4호선", "1005" => "5호선", "1006" => "6호선",
            "1007" => "7호선", "1008" => "8호선", "1009" => "9호선",
            "1061" => "중앙선", "1063" => "경의중앙선", "1065" => "공항철도",
            "1067" => "경춘선", "1075" => "수인분당선", "1077" => "신분당선",
            "1092" => "우이신설선", "1093" => "서해선", "1081" => "경강선",
            _ => $"노선 {Text(dto.SubwayId)}"
        };
        return new Metro(Text(dto.StationName), line, Text(dto.Direction),
            Text(dto.Destination), Text(dto.TrainNumber), Text(dto.TrainType),
            seconds, Text(dto.ArrivalMessage), Text(dto.LocationMessage), received);
    }
    private static string Text(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "정보 없음" : value.Trim();
}
