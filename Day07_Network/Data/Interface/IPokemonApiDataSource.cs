using Day07_Network.Data.DataSource;

namespace Day07_Network.Data.Interface;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);

}