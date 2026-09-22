using System.Text.Json.Serialization;

namespace Day09_Result.Data.DTOs;

public sealed class SubwayResponseDto
{
    [JsonPropertyName("errorMessage")]
    public ApiErrorDto? ErrorMessage { get; init; }

    [JsonPropertyName("realtimeArrivalList")]
    public List<SubwayArrivalDto>? RealtimeArrivalList { get; init; }
}

public sealed class ApiErrorDto
{
    [JsonPropertyName("status")]
    public int Status { get; init; }

    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}

public sealed class SubwayArrivalDto
{
    [JsonPropertyName("subwayId")]
    public string? SubwayId { get; init; }

    [JsonPropertyName("updnLine")]
    public string? UpDownLine { get; init; }

    [JsonPropertyName("trainLineNm")]
    public string? TrainLineName { get; init; }

    [JsonPropertyName("bstatnNm")]
    public string? Destination { get; init; }

    [JsonPropertyName("arvlMsg2")]
    public string? ArrivalMessage { get; init; }

    [JsonPropertyName("arvlMsg3")]
    public string? ArrivalMessageDetail { get; init; }

    [JsonPropertyName("recptnDt")]
    public string? ReceivedAt { get; init; }
}