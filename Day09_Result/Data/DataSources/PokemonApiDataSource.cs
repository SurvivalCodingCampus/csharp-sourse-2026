using System.Net;
using System.Net.Http.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.DataSources;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }
    }

    public async Task<Result<PokemonDto>> GetPokemonAsync(string pokemonName)
    {
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return Result<PokemonDto>.Failure("포켓몬 이름이 입력 되지 않음", PokemonErrorType.InvalidInput);
        }

        try
        {
            var formattedName = pokemonName.Trim().ToLowerInvariant();
            var response = await _httpClient.GetAsync($"pokemon/{formattedName}");

            // 포켓몬이 존재하지 않을 경우 null 반환
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return Result<PokemonDto>.Failure($"포켓몬 '{pokemonName}'을 찾을 수 없습니다.", PokemonErrorType.NotFound);
            }
            
            // 404 이외의 비정상 응답(500 등) 체크
            response.EnsureSuccessStatusCode();
            var dto = await response.Content.ReadFromJsonAsync<PokemonDto>();
            if (dto is null)
            {
                return Result<PokemonDto>.Failure("응답 데이터 역질렬화 불가", PokemonErrorType.SerializationError);
            }
            return Result<PokemonDto>.Success(dto);
        }
        catch (HttpRequestException ex)
        {
            // 네트워크 오류, 서버 장애 시 null 반환하여 게임 중단 방지
            return Result<PokemonDto>.Failure($"네트워크 연결 오류 : {ex.Message}",PokemonErrorType.NetworkError);
        }
        catch (Exception ex)
        {
            // 기타 JSON 파싱 실패 등의 오류 방어
            return Result<PokemonDto>.Failure($"알 수 없는 오류 발생 : {ex.Message}",PokemonErrorType.NetworkError);
        }
    }
}