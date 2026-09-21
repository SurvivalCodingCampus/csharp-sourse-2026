using Newtonsoft.Json;

namespace Day07_http.Data.Dto;

public class PokemonDto
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("sprites")]
    public SpritesDto? Sprites { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("types")]
    public List<PokemonTypeDto>? Types { get; set; }
}

public class SpritesDto
{
    [JsonProperty("other")]
    public OtherSpritesDto? Other { get; set; }
}

public class OtherSpritesDto
{
    [JsonProperty("official-artwork")]
    public OfficialArtworkDto? OfficialArtwork { get; set; }
}

public class OfficialArtworkDto
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }
}

public class PokemonTypeDto
{
    [JsonProperty("slot")]
    public int Slot { get; set; }

    [JsonProperty("type")]
    public PokemonTypeInfoDto? Type { get; set; }
}

public class PokemonTypeInfoDto
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}
