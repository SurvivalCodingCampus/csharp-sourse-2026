using Newtonsoft.Json;

namespace Day07_Http.Models;

public class PokemonTypeInfo
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

public class PokemonType
{
    [JsonProperty("type")]
    public PokemonTypeInfo? Type { get; set; }
}

public class PokemonStatInfo
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

public class PokemonStat
{
    [JsonProperty("base_stat")]
    public int BaseStat { get; set; }

    [JsonProperty("stat")]
    public PokemonStatInfo? Stat { get; set; }
}

public class Pokemon
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("types")]
    public List<PokemonType>? Types { get; set; }

    [JsonProperty("stats")]
    public List<PokemonStat>? Stats { get; set; }
}