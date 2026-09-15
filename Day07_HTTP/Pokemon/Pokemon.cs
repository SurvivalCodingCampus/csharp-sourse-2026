using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Day07_HTTP;


public class Pokemon
{
    [JsonProperty("id")] public int? Id { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("types")] public List<TypeSlot>? Types { get; set; }
    [JsonProperty("stats")] public List<StatSlot>? Stats { get; set; }
    [JsonProperty("sprites")] public SpritesContainer? Sprites { get; set; }
}

public class TypeSlot
{
    [JsonProperty("type")] public NamedApiResource? Type { get; set; }
}

public class StatSlot
{
    [JsonProperty("base_stat")] public int BaseStat { get; set; }
    [JsonProperty("stat")] public NamedApiResource? Stat { get; set; }
}

public class NamedApiResource
{
    [JsonProperty("name")] public string? Name { get; set; }
}

public class SpritesContainer
{
    [JsonProperty("other")] public OtherSprites? Other { get; set; }
}

public class OtherSprites
{
    [JsonProperty("official-artwork")] public OfficialArtwork? OfficialArtwork { get; set; }
}

public class OfficialArtwork
{
    [JsonProperty("front_default")] public string? FrontDefault { get; set; }
}