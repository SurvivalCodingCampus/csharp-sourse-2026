using Day07_http.Data.Dto;
using Day07_http.Models;

namespace Day07_http.Data.Mappers;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto dto)
    {
        return new Pokemon
        {
            Name = dto.Name,
            Height = dto.Height,
            Weight = dto.Weight,
            Sprites = dto.Sprites?.ToModel(),
            Types = dto.Types?.Select(type => type.ToModel()).ToList()
        };
    }

    private static Sprites ToModel(this SpritesDto dto)
    {
        return new Sprites
        {
            Other = dto.Other is null
                ? null
                : new OtherSprites
                {
                    OfficialArtwork = dto.Other.OfficialArtwork is null
                        ? null
                        : new PokemonSprites
                        {
                            OfficialArtworkUrl = dto.Other.OfficialArtwork.FrontDefault
                        }
                }
        };
    }

    private static PokemonType ToModel(this PokemonTypeDto dto)
    {
        return new PokemonType
        {
            Slot = dto.Slot,
            Type = dto.Type is null
                ? null
                : new PokemonTypeInfo
                {
                    Name = dto.Type.Name,
                    Url = dto.Type.Url
                }
        };
    }
}
