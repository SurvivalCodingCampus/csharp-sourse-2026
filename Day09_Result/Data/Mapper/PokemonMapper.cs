using Day09_Result.Data.DTOs;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

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