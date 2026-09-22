using System.Collections.Generic;
using System.Threading.Tasks;
using Day09_Result_Test.Data.DataSource;
using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repositories;
using NUnit.Framework;

namespace Day09_Result_Test.Data.Repositories;

[TestFixture]
[TestOf(typeof(PokemonRepository))]
public class PokemonRepositoryTest
{

    [Test]
    public async Task 과제1_기본Result_패턴작성()
    {
        IPokemonApiDataSource dataSource = new MockPokemonApiDataSource();
        IPokemonRepository repository = new PokemonRepository(dataSource);

        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("unknown");

        Assert.That(result, Is.InstanceOf<Result<Pokemon, PokemonError>.Error>());
        var errorResult = (Result<Pokemon, PokemonError>.Error)result;
        Assert.That(errorResult.error, Is.EqualTo(PokemonError.NotFound));
    }

    [Test]
    public async Task 과제2_TimeoutException()
    {
        IPokemonApiDataSource dataSource = new ErrorMockApiDataSource(ErrorMockApiDataSource.ErrorType.Timeout);
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("dittooo");
        
        Assert.That(result, Is.InstanceOf<Result<Pokemon, PokemonError>.Error>());
    }
    
    [Test]
    public async Task 과제2_JsonSerializationException()
    {
        IPokemonApiDataSource dataSource = new ErrorMockApiDataSource(ErrorMockApiDataSource.ErrorType.JsonSerialization);
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("ditto");
        
        Assert.That(result, Is.InstanceOf<Result<Pokemon, PokemonError>.Error>());
        var errorResult = (Result<Pokemon, PokemonError>.Error)result;
        Assert.That(errorResult.error, Is.EqualTo(PokemonError.JsonSerialization));
    }

    [Test]
    public async Task 과제3_Subway_NotFoundException()
    {
        ISubwayApiDataSource dataSource = new NotFoundSubwayMockApiDataSource();
        ISubwayRepository repository = new SubwayRepository(dataSource);
        
        Result<List<Subway>, SubwayError> result = await repository.GetSubwayByNameAsync("김포");
        
        Assert.That(result, Is.InstanceOf<Result<List<Subway>, SubwayError>.Error>());
        var errorResult = (Result<List<Subway>, SubwayError>.Error)result;
        Assert.That(errorResult.error, Is.EqualTo(SubwayError.NotFound));
    }

    [Test]
    public async Task 과제3_Subway_SucessTest()
    {
        ISubwayApiDataSource dataSource = new SuccessSubwayMockApiDataSource();
        ISubwayRepository repository = new SubwayRepository(dataSource);
        
        Result<List<Subway>, SubwayError> result = await repository.GetSubwayByNameAsync("서울");
        
        Assert.That(result, Is.InstanceOf<Result<List<Subway>, SubwayError>.Success>());

        var subwayResult = (Result<List<Subway>, SubwayError>.Success)result;
        List<Subway> subways = subwayResult.data;
        
        Assert.That(subways, Is.Not.Empty);
        Assert.That(subways[0].StatnNm, Is.EqualTo("서울"));
        Assert.That(subways[0].UpdnLine, Is.EqualTo("상행"));
        Assert.That(subways[0].TrainLineNm, Is.EqualTo("검암행 - 공덕방면"));
        Assert.That(subways[0].ArvlMsg2, Is.EqualTo("서울 출발"));
    }
}