namespace Day08_Dto_Mapper.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}