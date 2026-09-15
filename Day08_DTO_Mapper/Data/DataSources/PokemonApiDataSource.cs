using System.Net;
using System.Net.Http.Json;
using Day08_DTO_Mapper.Data.DTOs;

namespace Day08_DTO_Mapper.Data.DataSources;

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

    public async Task<PokemonDto?> GetPokemonAsync(string pokemonName)
    {
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return null;
        }

        try
        {
            var formattedName = pokemonName.Trim().ToLowerInvariant();
            var response = await _httpClient.GetAsync($"pokemon/{formattedName}");

            // 포켓몬이 존재하지 않을 경우 null 반환
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            // 404 이외의 비정상 응답(500 등) 체크
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PokemonDto>();
        }
        catch (HttpRequestException)
        {
            // 네트워크 오류, 서버 장애 시 null 반환하여 게임 중단 방지
            return null;
        }
        catch (Exception)
        {
            // 기타 JSON 파싱 실패 등의 오류 방어
            return null;
        }
    }
}