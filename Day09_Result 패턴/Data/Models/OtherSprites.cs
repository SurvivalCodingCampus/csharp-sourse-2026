namespace Day09_Result_패턴.Data.Models;

using Newtonsoft.Json;

public class OtherSprites
{
    [JsonProperty("official-artwork")]
    public OfficialArtwork? OfficialArtwork { get; set; }
}