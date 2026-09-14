namespace Day07_http_WithAI.Models;

using Newtonsoft.Json;

public class Pokemon
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("sprites")]
    public PokemonSprites? Sprites { get; set; }
}