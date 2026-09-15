using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Models;

namespace Day08_DTO_Mapper.Data.Mapper;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto dto)
    {
        return new Pokemon(
            dto.Id ?? 0,
            dto.Name.IsWhiteSpace() || dto.Name == null ? "missing.No" : dto.Name, 
            dto.Sprites?.FrontDefault ?? "",
            dto.Types?.Select(e => e.Type?.Name ?? "undefined").ToList() ?? new List<string>(["???"]));
    }
}