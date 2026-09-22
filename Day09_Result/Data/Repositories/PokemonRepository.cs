using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public class PokemonRepository(IPokemonApiDataSource dataSource) : IPokemonRepository
{
    public async Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await dataSource.GetPokemonAsync(pokemonName);

            switch (response.StatusCode)
            {
                case 200:
                    PokemonDto? pokemonDto = JsonSerializer.Deserialize<PokemonDto>(response.Body);
                    Pokemon? pokemon = pokemonDto?.ToModel(); 
                    return new Result<Pokemon, PokemonError>.Success(pokemon!);
                    
                case 404:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.NotFound);
                
                case -1:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.NetworkTimeout);
                
                default:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.Unknown);
            }
        }
        catch (Exception)
        {
            return new Result<Pokemon, PokemonError>.Failure(PokemonError.Unknown);
        }
    }
}