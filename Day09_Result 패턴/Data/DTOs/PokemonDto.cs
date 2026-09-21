namespace Day09_Result_패턴.Data.DTOs;

using System.Collections.Generic;
//과제1
public class PokemonDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? BaseExperience { get; set; }
    public int? Height { get; set; }
    public bool? IsDefault { get; set; }
    public int? Order { get; set; }
    public int? Weight { get; set; }
    
    public List<AbilityEntryDto>? Abilities { get; set; }
    public List<PastAbilityEntryDto>? PastAbilities { get; set; }
    public List<FormDto>? Forms { get; set; }
    public List<GameIndexDto>? GameIndices { get; set; }
    public List<HeldItemEntryDto>? HeldItems { get; set; }
    public string? LocationAreaEncounters { get; set; }
    public List<MoveEntryDto>? Moves { get; set; }
    public SpeciesDto? Species { get; set; }
    public SpritesDto? Sprites { get; set; }
    public CriesDto? Cries { get; set; }
    public List<StatEntryDto>? Stats { get; set; }
    public List<PastStatEntryDto>? PastStats { get; set; }
    public List<TypeEntryDto>? Types { get; set; }
    public List<PastTypeDto>? PastTypes { get; set; }
}

public class AbilityEntryDto
{
    public bool? IsHidden { get; set; }
    public int? Slot { get; set; }
    public NamedApiResourceDto? Ability { get; set; }
}

public class PastAbilityEntryDto
{
    public NamedApiResourceDto? Generation { get; set; }
    public List<AbilityDetailDto>? Abilities { get; set; }
}

public class AbilityDetailDto
{
    public bool? IsHidden { get; set; }
    public int? Slot { get; set; }
    public NamedApiResourceDto? Ability { get; set; }
}

public class FormDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class GameIndexDto
{
    public int? GameIndex { get; set; }
    public NamedApiResourceDto? Version { get; set; }
}

public class HeldItemEntryDto
{
    public NamedApiResourceDto? Item { get; set; }
    public List<VersionDetailDto>? VersionDetails { get; set; }
}

public class VersionDetailDto
{
    public int? Rarity { get; set; }
    public NamedApiResourceDto? Version { get; set; }
}

public class MoveEntryDto
{
    public NamedApiResourceDto? Move { get; set; }
    public List<VersionGroupDetailDto>? VersionGroupDetails { get; set; }
}

public class VersionGroupDetailDto
{
    public int? LevelLearnedAt { get; set; }
    public NamedApiResourceDto? VersionGroup { get; set; }
    public NamedApiResourceDto? MoveLearnMethod { get; set; }
    public int? Order { get; set; }
}

public class SpeciesDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class SpritesDto
{
    public string? BackDefault { get; set; }
    public string? BackFemale { get; set; }
    public string? BackShiny { get; set; }
    public string? BackShinyFemale { get; set; }
    public string? FrontDefault { get; set; }
    public string? FrontFemale { get; set; }
    public string? FrontShiny { get; set; }
    public string? FrontShinyFemale { get; set; }
    
    public Dictionary<string, object?>? Other { get; set; }
    public Dictionary<string, object?>? Versions { get; set; }
}

public class CriesDto
{
    public string? Latest { get; set; }
    public string? Legacy { get; set; }
}

public class StatEntryDto
{
    public int? BaseStat { get; set; }
    public int? Effort { get; set; }
    public NamedApiResourceDto? Stat { get; set; }
}

public class PastStatEntryDto
{
    public NamedApiResourceDto? Generation { get; set; }
    public List<StatEntryDto>? Stats { get; set; }
}

public class TypeEntryDto
{
    public int? Slot { get; set; }
    public NamedApiResourceDto? Type { get; set; }
}

public class PastTypeDto
{
    // 필요 시 구조 정의 가능
}

public class NamedApiResourceDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}