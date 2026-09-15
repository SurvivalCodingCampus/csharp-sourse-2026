using Day08_Dto_Mapper.Data.DataSources;


public interface IPokemonApiDataSource2{
    Task<Response2> GetPokemonAsync(string pokemonName);
}