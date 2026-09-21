namespace Day09_Result_패턴.Data.Models;

using Newtonsoft.Json;

public class PokemonSprites
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }

    [JsonProperty("other")]
    public OtherSprites? Other { get; set; }
}