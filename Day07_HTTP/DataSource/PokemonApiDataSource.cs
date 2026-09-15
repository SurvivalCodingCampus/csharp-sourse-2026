using Day07_HTTP.obj;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Day07_HTTP;

public class PokemonApiDataSource : IPokemonApiDataSource<Pokemon>
{
    //Data Get 데이터 불러오기
    private const string BaseUrl = "https://pokeapi.co/api/v2/";
    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<Response<TBody>> SendRequestAsync<TBody>(HttpMethod method, string url, object? data = null)
    {
        var request = new HttpRequestMessage(method, url);
        if (data != null)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/Json");
        }

        var httpResponse = await _httpClient.SendAsync(request);
        var jsonString = await httpResponse.Content.ReadAsStringAsync();
        var headers = httpResponse.Headers.ToDictionary(
            header => header.Key,
            header => string.Join(", ", header.Value)
        );
        TBody? bodyData = JsonConvert.DeserializeObject<TBody>(jsonString);
        return new Response<TBody>((int)httpResponse.StatusCode, headers, bodyData!);
    }

    public Task<Response<Pokemon>> GetByNameAsync(string pokemonName) => 
        SendRequestAsync<Pokemon>(HttpMethod.Get, $"{BaseUrl}pokemon/{pokemonName.ToLower()}");

    public Task<Response<Pokemon>> GetByIdAsync(int id) => 
        SendRequestAsync<Pokemon>(HttpMethod.Get, $"{BaseUrl}pokemon/{id}");
}
 
    
