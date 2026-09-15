using Newtonsoft.Json;

namespace Day07_http.Network.Models;
//원래 대로라면 각각의 폴더로 클래스를 담아야 하지만 Json은 한곳에 담을 수있다.
public class Pokemon {
    // https://pokeapi.co/api/v2/pokemon/pikachu
    // 위 파일의 경로에서 JsonProperty를 찾을 수 있는 경로
    // 📌⭐JsonProperty는 필수로 적기!
    [JsonProperty("name")]
    public string? Name { get; set;}


}
