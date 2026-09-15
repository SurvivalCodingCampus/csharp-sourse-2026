namespace Day07_Pokemon;

public interface IPokemonApiDataSource
{
    //포켓몬 검색결과가 없을수도 있으니 ?를 붙인다.
    Task<PokemonApiResponse?> GetPokemonAsync(string pokemonName);
}