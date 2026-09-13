using System.Text;
using Day07_http.Network.DataSources;
using Day07_http.Network.Interface;
using Newtonsoft.Json;

public class PokemonApiDataSource : IPokemonApiDataSource {
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    private readonly HttpClient _httpClient;

    //생성자
    public PokemonApiDataSource(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    //함수를 불러와서
    //값을 받아오고
    //실행하겠다
    private async Task<Response> SendRequestAsync(HttpMethod method, string url, object? data = null) {
        var request = new HttpRequestMessage(method, url);

        if (data != null) {
            var jsonContent = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        var httpResponse = await _httpClient.SendAsync(request);
        var jsonString = await httpResponse.Content.ReadAsStringAsync();
        var headers = httpResponse.Headers.ToDictionary(
            header => header.Key,
            header => string.Join(", ", header.Value)
        );
        return new Response((int)httpResponse.StatusCode, headers, jsonString);
    }
    
    // BaseUrl와 string값을 합쳐서 변수에 넣어주고
    // SendRequestAsync를 실행하도록 로직세우기
    //📌⭐ 견우와 직녀~
    public Task<Response> GetPokemonAsync(string pokemonName) {
        var Url = BaseUrl + pokemonName;
        return SendRequestAsync(HttpMethod.Get, Url);
    }
}


// p43 ,51