using Day08_DTO_Mapper.Data.DataSources;

namespace Day08_DTO_Mapper.Data.Interfaces;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}