using Newtonsoft.Json;

namespace Day07_http.Models;


public class Pokemon
{
    [JsonProperty("name")] 
    public string? Name { get; set; }

    [JsonProperty("sprites")]
    public Sprites? Sprites { get; set; }
    
    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("types")]
    public List<PokemonType>? Types { get; set; }
}
