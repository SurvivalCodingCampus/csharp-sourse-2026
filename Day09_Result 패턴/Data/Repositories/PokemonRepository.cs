using Day09_Result_패턴.Common.Errors.PokemonError;
using Day09_Result_패턴.Data.Common;
using Day09_Result_패턴.Data.DataSources;
using Day09_Result_패턴.Data.DTOs;
using Day09_Result_패턴.Data.Mapper;
using Day09_Result_패턴.Data.Models;

namespace Day09_Result_패턴.Data.Repositories;


public class PokemonRepository(IPokemonApiDataSource dataSource) : IPokemonRepository
{
    // private readonly IPokemonApiDataSource _dataSource;

    // public PokemonRepository(IPokemonApiDataSource dataSource)
    // {
    //     _dataSource = dataSource;
    // }

    public async Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName)
    {
        //과제 4
        try
        {
            Response response =
                await dataSource.GetPokemonAsync(pokemonName);

            switch (response.StatusCode)
            {
                case 200:
                    PokemonDto? pokemonDto = Newtonsoft.Json.JsonConvert.DeserializeObject<PokemonDto>(response.Body);
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

public class PokemonException : Exception;