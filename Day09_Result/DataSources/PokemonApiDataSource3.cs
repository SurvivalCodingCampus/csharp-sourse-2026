namespace Day09_Result_Pattern.Data.DataSources;

public class PokemonApiDataSource3 : IPokemonApiDataSource3 {
    private readonly HttpClient _httpClient = new HttpClient {
        Timeout = TimeSpan.FromSeconds(10)
    };

    public async Task<Response3> GetPokemonAsync(string name) {
        try {
            var res = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
            var body = await res.Content.ReadAsStringAsync();
            return new Response3((int)res.StatusCode, body);
        } catch (TaskCanceledException) {
            // HttpClient는 시간이 초과되면 이 예외를 던져서 TimeoutException으로 바꿔줘요
            throw new TimeoutException();
        }
    }
}