namespace Day09_Result_패턴.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}