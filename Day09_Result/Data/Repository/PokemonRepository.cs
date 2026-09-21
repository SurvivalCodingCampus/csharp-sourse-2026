using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Interfaces;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Day09_Result.Data.Repository;

public class PokemonRepository(IPokemonApiDataSource source) : IPokemonRepository
{
    public async Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await source.GetPokemonAsync(pokemonName);

            switch (response.StatusCode)
            {
                case 404:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.NotFound);
                case 200:
                    PokemonDto pokemonDto = JsonSerializer.Deserialize<PokemonDto>(response.Body)!;
                    return new Result<Pokemon, PokemonError>.Success(pokemonDto.ToModel());
                case -1:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.NetworkTimeout);
                default:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.Unknown);
            }
        }
        catch (Exception e)
        {
            switch (e)
            {
                case TimeoutException:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.NetworkTimeout);
                case JsonSerializationException:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.SerializationFailed);
                default:
                    return new Result<Pokemon, PokemonError>.Failure(PokemonError.Unknown);
            }
        }
    }
}