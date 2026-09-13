using Newtonsoft.Json;

namespace Day07_http.Network.Models;

public class Pokemon {
    // https://pokeapi.co/api/v2/pokemon/pikachu
    // 위 파일의 경로에서 JsonProperty를 찾을 수 있는 경로
    // 📌⭐JsonProperty는 필수로 적기!
    [JsonProperty("name")]
    public string? Name { get; set;}
}