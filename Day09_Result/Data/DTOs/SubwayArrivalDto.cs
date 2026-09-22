using System.Text.Json.Serialization;

namespace Day09_Result.Data.DTOs;

public class SubwayApiResponseDto
{
    [JsonPropertyName("errorMessage")]
    public ApiErrorMessageDto? ErrorMessage { get; set; }

    [JsonPropertyName("realtimeArrivalList")]
    public List<RealtimeArrivalItemDto>? RealtimeArrivalList { get; set; }
}

public class ApiErrorMessageDto
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class RealtimeArrivalItemDto
{
    [JsonPropertyName("subwayId")]
    public string? SubwayId { get; set; }       // 호선 ID (예: 1001 -> 1호선)

    [JsonPropertyName("statnNm")]
    public string StatnNm { get; set; } = string.Empty; // 역 이름

    [JsonPropertyName("trainLineNm")]
    public string TrainLineNm { get; set; } = string.Empty; // 방면 (예: 청량리행 - 시청방면)

    [JsonPropertyName("barvlDt")]
    public string? BarvlDt { get; set; }        // 도착 예정 시간(초)

    [JsonPropertyName("arvlMsg2")]
    public string ArvlMsg2 { get; set; } = string.Empty; // 도착 문구 (예: 전역 진입, 3분 후 도착)

    [JsonPropertyName("arvlMsg3")]
    public string? ArvlMsg3 { get; set; }       // 현재 위치 역 (예: 남영)
}