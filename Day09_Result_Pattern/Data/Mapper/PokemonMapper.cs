using Day09_Result_Pattern.Data.DTO;
using Day09_Result_Pattern.Data.Models;

namespace Day09_Result_Pattern.Data.Mapper;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto? dto)
    {
        if (dto == null)
        {
            return CreateDefaultPokemon();
        }

        var name = string.IsNullOrWhiteSpace(dto.Name)
            ? "Unknown"
            : dto.Name;

        var types = dto.Types?
                        .Select(type => type.Type?.Name)
                        .Where(name => !string.IsNullOrWhiteSpace(name))
                        .Select(name => name!)
                        .ToList()
                    ?? new List<string>();

        var height = dto.Height.GetValueOrDefault();

        if (height < 0)
        {
            height = 0;
        }

        var weight = dto.Weight.GetValueOrDefault();

        if (weight < 0)
        {
            weight = 0;
        }

        var attack =
            GetStat(dto, "attack");

        var defense =
            GetStat(dto, "defense");

        var speed =
            GetStat(dto, "speed");

        var specialAttack =
            GetStat(dto, "special-attack");

        var specialDefense =
            GetStat(dto, "special-defense");

        var imageUrl =
            dto.Sprites?.FrontDefault ?? "";

        return new Pokemon(
            name,
            types,
            height / 10.0,
            weight / 10.0,
            attack,
            defense,
            speed,
            specialAttack,
            specialDefense,
            imageUrl
        );
    }

    private static int GetStat(
        PokemonDto dto,
        string statName)
    {
        var stat = dto.Stats?
            .FirstOrDefault(
                stat => stat.Stat?.Name == statName
            );

        var value =
            stat?.BaseStat ?? 0;

        return value < 0
            ? 0
            : value;
    }

    private static Pokemon CreateDefaultPokemon()
    {
        return new Pokemon(
            "Unknown",
            new List<string>(),
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            ""
        );
    }
}