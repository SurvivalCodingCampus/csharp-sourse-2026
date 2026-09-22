using System.Net.Http.Json;
using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.DataSources;

public interface ISubwayApiDataSource
{
    // Result -> SubwayResult로 수정
    Task<SubwayResult<SubwayApiResponseDto>> GetRealtimeArrivalAsync(string stationName);
}

public class SubwayApiDataSource : ISubwayApiDataSource
{
    private readonly HttpClient _httpClient;

    public SubwayApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5/");
        }
    }

    // Result -> SubwayResult로 수정
    public async Task<SubwayResult<SubwayApiResponseDto>> GetRealtimeArrivalAsync(string stationName)
    {
        if (string.IsNullOrWhiteSpace(stationName))
        {
            return SubwayResult<SubwayApiResponseDto>.Failure("역 이름을 입력해 주세요.", SubwayErrorType.InvalidInput);
        }

        try
        {
            // 한글 역 이름 URL 인코딩 처리
            var encodedStation = Uri.EscapeDataString(stationName.Trim());
            var response = await _httpClient.GetAsync(encodedStation);

            if (!response.IsSuccessStatusCode)
            {
                return SubwayResult<SubwayApiResponseDto>.Failure($"서버 오류 응답 (상태 코드: {(int)response.StatusCode})", SubwayErrorType.NetworkError);
            }

            var dto = await response.Content.ReadFromJsonAsync<SubwayApiResponseDto>();
            if (dto is null)
            {
                return SubwayResult<SubwayApiResponseDto>.Failure("응답 역직렬화 실패", SubwayErrorType.SerializationError);
            }

            // 서울시 API 에러 응답 처리 (데이터가 없거나 역 이름이 유효하지 않은 경우)
            if (dto.ErrorMessage != null && dto.ErrorMessage.Status != 200)
            {
                return SubwayResult<SubwayApiResponseDto>.Failure($"'{stationName}'에 대한 정보를 찾을 수 없습니다. (사유: {dto.ErrorMessage.Message})", SubwayErrorType.StationNotFound);
            }

            if (dto.RealtimeArrivalList == null || dto.RealtimeArrivalList.Count == 0)
            {
                return SubwayResult<SubwayApiResponseDto>.Failure($"'{stationName}'역의 실시간 도착 정보가 없습니다.", SubwayErrorType.StationNotFound);
            }

            return SubwayResult<SubwayApiResponseDto>.Success(dto);
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            return SubwayResult<SubwayApiResponseDto>.Failure($"요청 시간이 초과되었습니다: {ex.Message}", SubwayErrorType.Timeout);
        }
        catch (JsonException ex)
        {
            return SubwayResult<SubwayApiResponseDto>.Failure($"JSON 형식 오류: {ex.Message}", SubwayErrorType.SerializationError);
        }
        catch (HttpRequestException ex)
        {
            return SubwayResult<SubwayApiResponseDto>.Failure($"네트워크 통신 오류: {ex.Message}", SubwayErrorType.NetworkError);
        }
        catch (Exception ex)
        {
            return SubwayResult<SubwayApiResponseDto>.Failure($"예기치 못한 오류: {ex.Message}", SubwayErrorType.NetworkError);
        }
    }
}