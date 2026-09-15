using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Interfaces;
using Day08_DTO_Mapper.Data.Models;
using System.Text.Json;
using Day08_DTO_Mapper.Data.Mapper;

namespace Day08_DTO_Mapper.Data.Repository;

public class PokemonRepository(IPokemonApiDataSource source) : IPokemonRepository
{
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await source.GetPokemonAsync(pokemonName);
            
            if (response.StatusCode == 404)
            {
                return new Pokemon(0, "MissingNo.", "", []);
            }
            if (response.StatusCode != 200)
            {
                return new Pokemon(-1, pokemonName, "", []);
            }
            PokemonDto? pokemonDto = JsonSerializer.Deserialize<PokemonDto>(response.Body);

            return pokemonDto?.ToModel();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}