using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Day08_DTO_Mapper;
using Newtonsoft.Json;

namespace Day08_DTO_Mapper;

public class DataSource(HttpClient httpClient) : IPokemonApiDataSource
{
    //Data Get 데이터 접근
    private const string BaseUrl = "https://pokeapi.co/api/v2/";
    private readonly HttpClient _httpClient = httpClient;
    

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

    public Task<Response<PokemonDTO>> GetByNameAsync(string pokemonName) => 
        SendRequestAsync<PokemonDTO?>(HttpMethod.Get, $"{BaseUrl}pokemon/{pokemonName.Trim().ToLowerInvariant()}");

    public Task<Response<PokemonDTO>> GetByIdAsync(int id) => 
        SendRequestAsync<PokemonDTO?>(HttpMethod.Get, $"{BaseUrl}pokemon/{id}");
}
 
    
