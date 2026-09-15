using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Day07_http.Models;

public class PokemonSprites
{
    [JsonProperty("front_default")]
    public string? OfficialArtworkUrl { get; set; }
}