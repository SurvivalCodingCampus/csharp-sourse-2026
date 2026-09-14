namespace Day07_http_WithAI.Models;

using Newtonsoft.Json;

public class OfficialArtwork
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }
}