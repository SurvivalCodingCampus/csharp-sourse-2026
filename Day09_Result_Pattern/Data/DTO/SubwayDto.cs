using Newtonsoft.Json;

namespace Day09_Result_Pattern.Data.DTO;

public class SubwayResponseDto
{
    [JsonProperty("errorMessage")]
    public SubwayErrorMessageDto? ErrorMessage { get; set; }

    [JsonProperty("realtimeArrivalList")]
    public List<SubwayArrivalDto>? RealtimeArrivalList { get; set; }
}

public class SubwayErrorMessageDto
{
    [JsonProperty("status")]
    public int? Status { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("total")]
    public int? Total { get; set; }
}

public class SubwayArrivalDto
{
    // 도착까지 남은 시간(초)
    [JsonProperty("barvlDt")]
    public string? ArrivalSeconds { get; set; }

    // 사용자에게 보여줄 도착 정보
    [JsonProperty("arvlMsg2")]
    public string? ArrivalMessage { get; set; }

    // 열차의 현재 위치
    [JsonProperty("arvlMsg3")]
    public string? CurrentLocation { get; set; }

    // 열차 도착 상태 코드
    [JsonProperty("arvlCd")]
    public string? ArrivalCode { get; set; }

    // 상행 / 하행
    [JsonProperty("updnLine")]
    public string? Direction { get; set; }

    // 광운대행, 인천행 등
    [JsonProperty("trainLineNm")]
    public string? TrainLineName { get; set; }

    // 종착역
    [JsonProperty("bstatnNm")]
    public string? TerminalStation { get; set; }

    // 조회하고 있는 역
    [JsonProperty("statnNm")]
    public string? StationName { get; set; }
}