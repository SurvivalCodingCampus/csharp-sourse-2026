using Day08_DTO_Mapper.Data.DTO;
using Newtonsoft.Json;

namespace Day08_DTO_Mapper.Data.DataSources;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private const string BaseUrl =
        "https://pokeapi.co/api/v2/pokemon";

    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonDto?> GetPokemonAsync(
        string pokemonName)
    {
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return null;
        }

        try
        {
            var name = pokemonName
                .Trim()
                .ToLowerInvariant();

            var url =
                $"{BaseUrl}/{name}";

            var response =
                await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json =
                await response.Content
                    .ReadAsStringAsync();

            return JsonConvert
                .DeserializeObject<PokemonDto>(
                    json
                );
        }
        catch
        {
            return null;
        }
    }
}