namespace Day07_http.Data.Models;

using System.Text.Json.Serialization;

public class Pokemon
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("sprites")]
    public Sprites Sprites { get; set; } = new();

    // 편의상 최상위에서 바로 공식 아트워크에 접근하고 싶을 때 사용
    [JsonIgnore]
    public OfficialArtwork? OfficialArtwork => Sprites?.Other?.OfficialArtwork;
    
}

public class Sprites
{
    [JsonPropertyName("other")]
    public Other Other { get; set; } = new();
}

public class Other
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtwork OfficialArtwork { get; set; } = new();
}

public class OfficialArtwork
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }

    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }
}