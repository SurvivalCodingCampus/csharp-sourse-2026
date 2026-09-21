using Newtonsoft.Json;

namespace Day09_Result_Pattern.Data.DTO;

public class PokemonDto3
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("base_experience")]
    public int? BaseExperience { get; set; }

    [JsonProperty("height")]
    public int? Height { get; set; }

    [JsonProperty("is_default")]
    public bool? IsDefault { get; set; }

    [JsonProperty("order")]
    public int? Order { get; set; }

    [JsonProperty("weight")]
    public int? Weight { get; set; }

    [JsonProperty("abilities")]
    public List<PokemonAbilityDto>? Abilities { get; set; }

    [JsonProperty("forms")]
    public List<NamedApiResourceDto>? Forms { get; set; }

    [JsonProperty("moves")]
    public List<PokemonMoveDto>? Moves { get; set; }

    [JsonProperty("species")]
    public NamedApiResourceDto? Species { get; set; }

    [JsonProperty("sprites")]
    public PokemonSpritesDto? Sprites { get; set; }

    [JsonProperty("stats")]
    public List<PokemonStatDto>? Stats { get; set; }

    [JsonProperty("types")]
    public List<PokemonTypeDto>? Types { get; set; }
}

public class PokemonAbilityDto
{
    [JsonProperty("ability")]
    public NamedApiResourceDto? Ability { get; set; }

    [JsonProperty("is_hidden")]
    public bool? IsHidden { get; set; }

    [JsonProperty("slot")]
    public int? Slot { get; set; }
}

public class PokemonMoveDto
{
    [JsonProperty("move")]
    public NamedApiResourceDto? Move { get; set; }

    [JsonProperty("version_group_details")]
    public List<VersionGroupDetailDto>? VersionGroupDetails { get; set; }
}

public class VersionGroupDetailDto
{
    [JsonProperty("level_learned_at")]
    public int? LevelLearnedAt { get; set; }

    [JsonProperty("move_learn_method")]
    public NamedApiResourceDto? MoveLearnMethod { get; set; }

    [JsonProperty("version_group")]
    public NamedApiResourceDto? VersionGroup { get; set; }
}

public class PokemonStatDto
{
    [JsonProperty("base_stat")]
    public int? BaseStat { get; set; }

    [JsonProperty("effort")]
    public int? Effort { get; set; }

    [JsonProperty("stat")]
    public NamedApiResourceDto? Stat { get; set; }
}

public class PokemonTypeDto
{
    [JsonProperty("slot")]
    public int? Slot { get; set; }

    [JsonProperty("type")]
    public NamedApiResourceDto? Type { get; set; }
}

public class NamedApiResourceDto
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}

public class PokemonSpritesDto
{
    [JsonProperty("back_default")]
    public string? BackDefault { get; set; }

    [JsonProperty("back_female")]
    public string? BackFemale { get; set; }

    [JsonProperty("back_shiny")]
    public string? BackShiny { get; set; }

    [JsonProperty("back_shiny_female")]
    public string? BackShinyFemale { get; set; }

    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }

    [JsonProperty("front_female")]
    public string? FrontFemale { get; set; }

    [JsonProperty("front_shiny")]
    public string? FrontShiny { get; set; }

    [JsonProperty("front_shiny_female")]
    public string? FrontShinyFemale { get; set; }
}