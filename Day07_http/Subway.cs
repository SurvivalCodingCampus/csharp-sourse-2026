using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Day07_http;

public class Subway
{
    private const string ApiKey = "sample";
    private static readonly HttpClient HttpClient = new();

    
    public async Task<List<SubwayArrival>> GetArrivalsAsync(string stationName)
    {
        var encodedName = Uri.EscapeDataString(stationName);
        var url = $"http://swopenapi.seoul.go.kr/api/subway/{ApiKey}/json/realtimeStationArrival/0/5/{encodedName}";

        string json;
        try
        {
            json = await HttpClient.GetStringAsync(url);
        }
        catch (HttpRequestException ex)
        {
            throw new SubwayApiException($"지하철 API 요청에 실패했습니다: {ex.Message}", ex);
        }

        JsonDocument document;
        try
        {
            document = JsonDocument.Parse(json);
        }
        catch (JsonException ex)
        {
            throw new SubwayApiException("지하철 API 응답을 해석할 수 없습니다.", ex);
        }

        using (document)
        {
            var root = document.RootElement;

           
            if (root.TryGetProperty("errorMessage", out var errorMessageElement))
            {
                var code = errorMessageElement.GetProperty("code").GetString();

                if (code != "INFO-000")
                {
                    var message = errorMessageElement.GetProperty("message").GetString();
                    throw new SubwayApiException(
                        $"'{stationName}' 역 정보를 가져오지 못했습니다. (코드: {code}, 메시지: {message})");
                }

                if (!root.TryGetProperty("realtimeArrivalList", out var listElement)
                    || listElement.ValueKind != JsonValueKind.Array
                    || listElement.GetArrayLength() == 0)
                {
                    throw new SubwayApiException($"'{stationName}' 역의 도착 정보가 없습니다. 역 이름을 다시 확인해 주세요.");
                }

                return JsonSerializer.Deserialize<List<SubwayArrival>>(listElement.GetRawText()) ?? [];
            }

            
            if (root.TryGetProperty("code", out var flatCodeElement))
            {
                var code = flatCodeElement.GetString();
                var message = root.TryGetProperty("message", out var messageElement) ? messageElement.GetString() : null;
                throw new SubwayApiException(
                    $"'{stationName}' 역 정보를 가져오지 못했습니다. (코드: {code}, 메시지: {message})");
            }

            throw new SubwayApiException("지하철 API로부터 알 수 없는 형식의 응답을 받았습니다.");
        }
    }

   
    public async Task PrintArrivalsAsync(string stationName)
    {
        try
        {
            var arrivals = await GetArrivalsAsync(stationName);

            Console.WriteLine($"===== '{stationName}' 역 실시간 도착 정보 =====");
            foreach (var arrival in arrivals)
            {
                Console.WriteLine(
                    $"[{arrival.UpdnLine}] {arrival.TrainLineNm} - {arrival.ArvlMsg2} ({arrival.ArvlMsg3})");
            }
        }
        catch (SubwayApiException ex)
        {
            Console.WriteLine($"에러 발생: {ex.Message}");
        }
    }
}


public class SubwayApiException : Exception
{
    public SubwayApiException(string message) : base(message)
    {
    }

    public SubwayApiException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class SubwayArrival
{
    [JsonPropertyName("subwayId")]
    public string? SubwayId { get; set; }

    
    [JsonPropertyName("updnLine")]
    public string? UpdnLine { get; set; }

    
    [JsonPropertyName("trainLineNm")]
    public string? TrainLineNm { get; set; }

    [JsonPropertyName("statnNm")]
    public string? StatnNm { get; set; }

    
    [JsonPropertyName("arvlMsg2")]
    public string? ArvlMsg2 { get; set; }

   
    [JsonPropertyName("arvlMsg3")]
    public string? ArvlMsg3 { get; set; }
}
    