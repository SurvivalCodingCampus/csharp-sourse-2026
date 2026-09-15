using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class PokemonApiDataSource
{
    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonDto?> FetchPokemonAsync(string pokemonName)
    {
        string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}";

        try
        {
            string jsonResponse = await _httpClient.GetStringAsync(url);
            // JSON 문자열을 방어적인 DTO 객체로 역직렬화
            return JsonConvert.DeserializeObject<PokemonDto>(jsonResponse);
        }
        catch
        {
            // 네트워크 오류나 파싱 오류 발생 시 예외를 던지지 않고 null을 반환하여 방어
            return null;
        }
    }
}