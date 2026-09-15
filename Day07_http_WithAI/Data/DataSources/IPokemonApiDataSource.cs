namespace Day07_http_WithAI.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}