namespace Day06_Repository.DataSources;

public interface IPokeApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}

