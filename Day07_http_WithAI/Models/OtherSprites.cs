namespace Day07_http_WithAI.Models;

using Newtonsoft.Json;

public class OtherSprites
{
    [JsonProperty("official-artwork")]
    public OfficialArtwork? OfficialArtwork { get; set; }
}