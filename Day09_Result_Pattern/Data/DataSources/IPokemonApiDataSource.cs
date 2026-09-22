using Day09_Result_Pattern.Data.DTO;

namespace Day09_Result_Pattern.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response<PokemonDto>> GetPokemonAsync(
        string pokemonName
    );
}