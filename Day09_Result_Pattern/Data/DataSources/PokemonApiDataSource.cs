using Day09_Result_Pattern.Data.DTO;
using Newtonsoft.Json;

namespace Day09_Result_Pattern.Data.DataSources;

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

    public async Task<Response<PokemonDto>> GetPokemonAsync(
        string pokemonName)
    {
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return new Response<PokemonDto>
            {
                StatusCode = 400,
                Body = null
            };
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
                return new Response<PokemonDto>
                {
                    StatusCode = (int)response.StatusCode,
                    Body = null
                };
            }

            var json =
                await response.Content
                    .ReadAsStringAsync();

            var dto =
                JsonConvert.DeserializeObject<PokemonDto>(
                    json
                );

            return new Response<PokemonDto>
            {
                StatusCode = 200,
                Body = dto
            };
        }
        catch (TaskCanceledException)
        {
            return new Response<PokemonDto>
            {
                StatusCode = -1,
                Body = null
            };
        }
        catch
        {
            return new Response<PokemonDto>
            {
                StatusCode = 0,
                Body = null
            };
        }
    }
}