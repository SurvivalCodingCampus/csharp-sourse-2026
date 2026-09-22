using Day09_Result.Data.DTOs;     
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto dto)
    {
        string safeName = string.IsNullOrWhiteSpace(dto.Name) ? "Unknown" : dto.Name;

        string safeImageUrl = dto.Sprites?.Other?.OfficialArtwork?.FrontDefault
                              ?? dto.Sprites?.FrontDefault
                              ?? "default_img.png";

        return new Pokemon(Name: safeName, ImageUrl: safeImageUrl);
    }
}