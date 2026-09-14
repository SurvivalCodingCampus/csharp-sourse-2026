using Newtonsoft.Json;

namespace Day07_http.Models;

// ── sprites 관련 ──
// JSON 최상위 "sprites" 객체에 해당 (그 안에 "other"가 있고, 그 안에 "official-artwork"가 있음)
public class Sprites
{
    [JsonProperty("other")]
    public OtherSprites? Other { get; set; }
}

// JSON의 "other" 객체에 해당
public class OtherSprites
{
    [JsonProperty("official-artwork")]
    public PokemonSprites? OfficialArtwork { get; set; }
}

// JSON의 "official-artwork" 객체에 해당
public class PokemonSprites
{
    [JsonProperty("front_default")]
    public string? OfficialArtworkUrl { get; set; }
}

// ── types 관련 ──
// JSON의 "types" 배열 안 원소에 해당
public class PokemonType
{
    [JsonProperty("slot")]
    public int Slot { get; set; }

    [JsonProperty("type")]
    public PokemonTypeInfo? Type { get; set; }
}

// "type" 안의 { "name": ..., "url": ... } 객체에 해당
public class PokemonTypeInfo
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}
