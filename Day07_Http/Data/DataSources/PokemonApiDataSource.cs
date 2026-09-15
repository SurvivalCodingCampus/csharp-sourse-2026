using System.Text;
using Day07_Http.Data.Interfaces;
using Newtonsoft.Json;

namespace Day07_Http.Data.DataSources;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private const string BaseUrl =
        "https://pokeapi.co/api/v2/pokemon";

    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<Response> SendRequestAsync(
        HttpMethod method,
        string url,
        object? data = null)
    {
        var request = new HttpRequestMessage(method, url);

        if (data != null)
        {
            var jsonContent =
                JsonConvert.SerializeObject(data);

            request.Content =
                new StringContent(
                    jsonContent,
                    Encoding.UTF8,
                    "application/json"
                );
        }

        var httpResponse =
            await _httpClient.SendAsync(request);

        var jsonString =
            await httpResponse.Content.ReadAsStringAsync();

        var headers =
            httpResponse.Headers.ToDictionary(
                header => header.Key,
                header => string.Join(", ", header.Value)
            );

        return new Response(
            (int)httpResponse.StatusCode,
            headers,
            jsonString
        );
    }

    public Task<Response> GetPokemonAsync(string pokemonName) =>
        SendRequestAsync(
            HttpMethod.Get,
            $"{BaseUrl}/{pokemonName}"
        );
}