using System.Text.Json;
using Day08_Dto_Mapper.Data.DataSources;
using Day08_Dto_Mapper.Data.Mapper;
using Day08_Dto_Mapper.Data.Models;

namespace Day08_Dto_Mapper.Data.Repositories;

public class PokemonRepository(IPokemonApiDataSource dataSource) : IPokemonRepository
{
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await dataSource.GetPokemonAsync(pokemonName);

            if (response.StatusCode != 200)
            {
                return null;
            }

            PokemonDto? pokemonDto = JsonSerializer.Deserialize<PokemonDto>(response.Body);
            return pokemonDto.ToModel();
        }
        catch (Exception)
        {
            throw new ArgumentException("Pokemon not found");
        }
    }
}