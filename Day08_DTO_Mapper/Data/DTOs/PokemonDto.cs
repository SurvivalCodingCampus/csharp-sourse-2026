using System.Text.Json.Serialization;

namespace Day08_DTO_Mapper.Data.DTOs;

public class PokemonDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("sprites")]
    public PokemonSpritesDto Sprites { get; set; } = new();

    [JsonPropertyName("types")]
    public List<PokemonTypeSlotDto> Types { get; set; } = new();
}

public class PokemonSpritesDto
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}

public class PokemonTypeSlotDto
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public NamedApiResourceDto Type { get; set; } = new();
}

public class NamedApiResourceDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}