namespace Day07_http_WithAI.Data.DataSources;

using System.Net.Http;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private const string BaseUrl =
        "https://pokeapi.co/api/v2/pokemon";

    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        string url = $"{BaseUrl}/{pokemonName}";

        HttpResponseMessage httpResponse =
            await _httpClient.GetAsync(url); //중요

        string body =
            await httpResponse.Content.ReadAsStringAsync();

        Dictionary<string, string> headers =
            httpResponse.Headers.ToDictionary(
                header => header.Key,
                header => string.Join(", ", header.Value)
            );

        return new Response(
            (int)httpResponse.StatusCode,
            headers,
            body
        );
    }
    
    //리팩토링
    
    // public Task<Response> GetPokemonAsync(string pokemonName)
    // {
    //     string url = $"{BaseUrl}/{pokemonName}";
    //
    //     return SendRequestAsync(
    //         HttpMethod.Get,
    //         url
    //     );
    // }
    
}