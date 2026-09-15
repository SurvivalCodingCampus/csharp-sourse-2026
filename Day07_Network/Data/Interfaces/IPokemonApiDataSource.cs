using Day07_Network.Data.DataSources;

namespace Day07_Network.Data.Interfaces;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);

}