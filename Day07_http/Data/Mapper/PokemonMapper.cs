using Day07_http.Data.DTOs;
using Day07_http.Data.Models;

namespace Day07_http.Data.Mapper;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto dto)
    {
        return new Pokemon(
            Name: dto.Name!, 
            ImageUrl: dto.Sprites!.FrontDefault!);
    }
}