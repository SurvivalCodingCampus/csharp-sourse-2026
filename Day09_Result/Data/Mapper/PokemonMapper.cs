using Day09_Result.Data.Models;

namespace Day09_Result.Data.Mapper;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto? dto)
    {
        if (dto == null)
        {
            return new Pokemon(Name: "Unknown", ImageUrl: "default_img.png");
        }

        try
        {
            string safeName = string.IsNullOrWhiteSpace(dto.Name) ? "Unknown" : dto.Name;
            
            string safeImageUrl = dto.Sprites?.Other?.OfficialArtwork?.FrontDefault 
                                  ?? dto.Sprites?.FrontDefault 
                                  ?? "default_img.png";
            
            if (string.IsNullOrWhiteSpace(safeImageUrl))
            {
                safeImageUrl = "default_img.png";
            }
            
            return new Pokemon(
                Name: safeName,
                ImageUrl: safeImageUrl
            );
        }
        catch (Exception)
        {
            return new Pokemon(Name: "Error", ImageUrl: "default_img.png");
        }
    }
}