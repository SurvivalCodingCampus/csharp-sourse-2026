using System.Text.Json;
using Day07_http.Data.Common;
using Day07_http.Data.Common.Errors;
using Day07_http.Data.DataSources;
using Day07_http.Data.DTOs;
using Day07_http.Data.Mapper;
using Day07_http.Data.Models;
using Day07_http.Data.Repositories;

namespace Day07_http.Data.Interfaces;

public class PokemonRepository(IPokemonApiDataSource dataSource): IPokemonRepository
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

            
        } catch (Exception)
        {
            return new Result<Pokemon, PokemonError>.Failure(PokemonError.Unknown);
        }
    }
}

public class PokemonException : Exception;