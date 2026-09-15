using Newtonsoft.Json;

namespace Day06_Repository.Models;

public record PokemonDto(
    int Id,
    string Name,
    int Height,
    int Weight,
    SpritesDto? Sprites
);

public record SpritesDto(
    [property: JsonProperty("front_default")] string? FrontDefault
);