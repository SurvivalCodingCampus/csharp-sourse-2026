using Newtonsoft.Json;
using System.Collections.Generic;

public class PokemonDto
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("height")]
    public int? Height { get; set; }

    [JsonProperty("weight")]
    public int? Weight { get; set; }

    [JsonProperty("sprites")]
    public PokemonSpritesDto? Sprites { get; set; }
}

public class PokemonSpritesDto
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }
}