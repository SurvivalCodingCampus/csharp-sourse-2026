using Day07_Http.Data.DataSources;

namespace Day07_Http.Data.Interfaces;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}