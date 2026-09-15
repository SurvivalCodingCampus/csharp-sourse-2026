namespace Day07_http_WithAI.Models;

using Newtonsoft.Json;

public class PokemonSprites
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }

    [JsonProperty("other")]
    public OtherSprites? Other { get; set; }
}