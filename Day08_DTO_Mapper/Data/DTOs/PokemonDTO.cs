using System.Text.Json.Serialization;

namespace Day08_DTO_Mapper.Data.DTOs;

public class PokemonDto
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("base_experience")]
    public int? BaseExperience { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("is_default")]
    public bool? IsDefault { get; set; }

    [JsonPropertyName("order")]
    public int? Order { get; set; }

    [JsonPropertyName("weight")]
    public int? Weight { get; set; }

    [JsonPropertyName("abilities")]
    public List<AbilityDto>? Abilities { get; set; }

    [JsonPropertyName("past_abilities")]
    public List<PastAbilityDto>? PastAbilities { get; set; }

    [JsonPropertyName("forms")]
    public List<NamedApiResourceDto>? Forms { get; set; }

    [JsonPropertyName("game_indices")]
    public List<GameIndexDto>? GameIndices { get; set; }

    [JsonPropertyName("held_items")]
    public List<HeldItemDto>? HeldItems { get; set; }

    [JsonPropertyName("location_area_encounters")]
    public string? LocationAreaEncounters { get; set; }

    [JsonPropertyName("moves")]
    public List<MoveDto>? Moves { get; set; }

    [JsonPropertyName("species")]
    public NamedApiResourceDto? Species { get; set; }

    [JsonPropertyName("sprites")]
    public SpritesDto? Sprites { get; set; }

    [JsonPropertyName("cries")]
    public CriesDto? Cries { get; set; }

    [JsonPropertyName("stats")]
    public List<StatDto>? Stats { get; set; }

    [JsonPropertyName("past_stats")]
    public List<PastStatDto>? PastStats { get; set; }

    [JsonPropertyName("types")]
    public List<TypeDto>? Types { get; set; }

    [JsonPropertyName("past_types")]
    public List<PastTypeDto>? PastTypes { get; set; }
}


// ============================================================
// 공통 API Resource
// ============================================================

public class NamedApiResourceDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}


// ============================================================
// Ability
// ============================================================

public class AbilityDto
{
    [JsonPropertyName("is_hidden")]
    public bool? IsHidden { get; set; }

    [JsonPropertyName("slot")]
    public int? Slot { get; set; }

    [JsonPropertyName("ability")]
    public NamedApiResourceDto? Ability { get; set; }
}

public class PastAbilityDto
{
    [JsonPropertyName("generation")]
    public NamedApiResourceDto? Generation { get; set; }

    [JsonPropertyName("abilities")]
    public List<AbilityDto>? Abilities { get; set; }
}


// ============================================================
// Game Index
// ============================================================

public class GameIndexDto
{
    [JsonPropertyName("game_index")]
    public int? GameIndex { get; set; }

    [JsonPropertyName("version")]
    public NamedApiResourceDto? Version { get; set; }
}


// ============================================================
// Held Item
// ============================================================

public class HeldItemDto
{
    [JsonPropertyName("item")]
    public NamedApiResourceDto? Item { get; set; }

    [JsonPropertyName("version_details")]
    public List<VersionDetailDto>? VersionDetails { get; set; }
}

public class VersionDetailDto
{
    [JsonPropertyName("rarity")]
    public int? Rarity { get; set; }

    [JsonPropertyName("version")]
    public NamedApiResourceDto? Version { get; set; }
}


// ============================================================
// Move
// ============================================================

public class MoveDto
{
    [JsonPropertyName("move")]
    public NamedApiResourceDto? Move { get; set; }

    [JsonPropertyName("version_group_details")]
    public List<VersionGroupDetailDto>? VersionGroupDetails { get; set; }
}

public class VersionGroupDetailDto
{
    [JsonPropertyName("level_learned_at")]
    public int? LevelLearnedAt { get; set; }

    [JsonPropertyName("version_group")]
    public NamedApiResourceDto? VersionGroup { get; set; }

    [JsonPropertyName("move_learn_method")]
    public NamedApiResourceDto? MoveLearnMethod { get; set; }

    [JsonPropertyName("order")]
    public int? Order { get; set; }
}


// ============================================================
// Cries
// ============================================================

public class CriesDto
{
    [JsonPropertyName("latest")]
    public string? Latest { get; set; }

    [JsonPropertyName("legacy")]
    public string? Legacy { get; set; }
}


// ============================================================
// Stats
// ============================================================

public class StatDto
{
    [JsonPropertyName("base_stat")]
    public int? BaseStat { get; set; }

    [JsonPropertyName("effort")]
    public int? Effort { get; set; }

    [JsonPropertyName("stat")]
    public NamedApiResourceDto? Stat { get; set; }
}

public class PastStatDto
{
    [JsonPropertyName("generation")]
    public NamedApiResourceDto? Generation { get; set; }

    [JsonPropertyName("stats")]
    public List<StatDto>? Stats { get; set; }
}


// ============================================================
// Type
// ============================================================

public class TypeDto
{
    [JsonPropertyName("slot")]
    public int? Slot { get; set; }

    [JsonPropertyName("type")]
    public NamedApiResourceDto? Type { get; set; }
}

public class PastTypeDto
{
    [JsonPropertyName("generation")]
    public NamedApiResourceDto? Generation { get; set; }

    [JsonPropertyName("types")]
    public List<TypeDto>? Types { get; set; }
}


// ============================================================
// Sprites
// ============================================================

public class SpritesDto
{
    [JsonPropertyName("back_default")]
    public string? BackDefault { get; set; }

    [JsonPropertyName("back_female")]
    public string? BackFemale { get; set; }

    [JsonPropertyName("back_shiny")]
    public string? BackShiny { get; set; }

    [JsonPropertyName("back_shiny_female")]
    public string? BackShinyFemale { get; set; }

    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }

    [JsonPropertyName("front_female")]
    public string? FrontFemale { get; set; }

    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }

    [JsonPropertyName("front_shiny_female")]
    public string? FrontShinyFemale { get; set; }

    [JsonPropertyName("other")]
    public Dictionary<string, SpriteVariantDto>? Other { get; set; }

    [JsonPropertyName("versions")]
    public Dictionary<string, Dictionary<string, SpriteVariantDto>>? Versions
    {
        get;
        set;
    }
}


// ============================================================
// Sprite Variant
// PokeAPI 세대별 Sprite에서 등장 가능한 속성을 통합
// ============================================================

public class SpriteVariantDto
{
    [JsonPropertyName("back_default")]
    public string? BackDefault { get; set; }

    [JsonPropertyName("back_female")]
    public string? BackFemale { get; set; }

    [JsonPropertyName("back_shiny")]
    public string? BackShiny { get; set; }

    [JsonPropertyName("back_shiny_female")]
    public string? BackShinyFemale { get; set; }

    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }

    [JsonPropertyName("front_female")]
    public string? FrontFemale { get; set; }

    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }

    [JsonPropertyName("front_shiny_female")]
    public string? FrontShinyFemale { get; set; }

    [JsonPropertyName("back_gray")]
    public string? BackGray { get; set; }

    [JsonPropertyName("front_gray")]
    public string? FrontGray { get; set; }

    [JsonPropertyName("back_transparent")]
    public string? BackTransparent { get; set; }

    [JsonPropertyName("front_transparent")]
    public string? FrontTransparent { get; set; }

    [JsonPropertyName("back_shiny_transparent")]
    public string? BackShinyTransparent { get; set; }

    [JsonPropertyName("front_shiny_transparent")]
    public string? FrontShinyTransparent { get; set; }

    [JsonPropertyName("animated")]
    public SpriteVariantDto? Animated { get; set; }
}