using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Day07_http.Models;

public class OtherSprites
{
    [JsonProperty("official-artwork")]
    public PokemonSprites? OfficialArtwork { get; set; }
}