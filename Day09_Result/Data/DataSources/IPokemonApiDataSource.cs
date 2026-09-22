namespace Day09_Result.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}