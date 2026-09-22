using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Day08_DTO_Mapper;
using Day09_Result_Test.Reposetory;
using Day09_Result.Common;
using Day09_Result.Common.Error;
using Xunit;

public class PokemonRepositoryTests01
{
    private static readonly HttpClient client = new HttpClient();
    //404에러 분류 테스트
    [Fact]
    public async Task RequestTimeOut()
    {
        // Arrange: "ditooo" 요청에 408를 반환하도록 설정
        var dataSourceMock = new TimeOutMockDataSource();

        

        var repository = new Repository(new DataSource(client));

        // Act: 실제 Repository의 오류 분류 로직 실행
        var result = await repository.GetPokemonByNameAsync("ditooo");

        // Assert: 실패 결과이며, 오류가 NotFound인지 확인
        var error = Assert.IsType<Result<Pokemon, PokemonError>.Error>(result);

        Assert.Equal(PokemonError.NetworkTimeOut, error.error);
    }
}