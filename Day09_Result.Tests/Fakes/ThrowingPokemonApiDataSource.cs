using Day09_Result.Data.DataSources;

namespace Day09_Result.Tests.Fakes;

// GetPokemonAsync가 호출되면 생성자로 전달받은 예외를 그대로 던지는 Fake DataSource.
// 예외 종류별로 클래스를 따로 만들지 않고, 하나의 클래스를 여러 시나리오에서 재사용한다.
public class ThrowingPokemonApiDataSource(Exception exceptionToThrow) : IPokemonApiDataSource
{
    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        throw exceptionToThrow;
    }
}