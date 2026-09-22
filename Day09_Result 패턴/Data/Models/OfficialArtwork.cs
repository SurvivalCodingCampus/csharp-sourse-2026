namespace Day09_Result_패턴.Data.Models;

using Newtonsoft.Json;

public class OfficialArtwork
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }
}