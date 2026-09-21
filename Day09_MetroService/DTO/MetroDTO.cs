using Newtonsoft.Json;
namespace Day09_MetroService.DTO;

// API 원본 구조를 받는 DTO: 모든 속성은 nullable입니다.
public sealed class MetroDTO
{
    [JsonProperty("errorMessage")] public ApiMessageDTO? ErrorMessage { get; set; }
    [JsonProperty("realtimeArrivalList")] public List<ArrivalDTO?>? Arrivals { get; set; }
    // 오류 응답이 최상위 code/message로 오는 경우도 처리합니다.
    [JsonProperty("code")] public string? Code { get; set; }
    [JsonProperty("message")] public string? Message { get; set; }
}
public sealed class ApiMessageDTO
{
    [JsonProperty("status")] public int? Status { get; set; }
    [JsonProperty("code")] public string? Code { get; set; }
    [JsonProperty("message")] public string? Message { get; set; }
}
public sealed class ArrivalDTO
{
    [JsonProperty("statnNm")] public string? StationName { get; set; }
    [JsonProperty("subwayId")] public string? SubwayId { get; set; }
    [JsonProperty("updnLine")] public string? Direction { get; set; }
    [JsonProperty("trainLineNm")] public string? Destination { get; set; }
    [JsonProperty("btrainNo")] public string? TrainNumber { get; set; }
    [JsonProperty("btrainSttus")] public string? TrainType { get; set; }
    [JsonProperty("barvlDt")] public string? ArrivalSeconds { get; set; }
    [JsonProperty("arvlMsg2")] public string? ArrivalMessage { get; set; }
    [JsonProperty("arvlMsg3")] public string? LocationMessage { get; set; }
    [JsonProperty("recptnDt")] public string? ReceivedAt { get; set; }
}
