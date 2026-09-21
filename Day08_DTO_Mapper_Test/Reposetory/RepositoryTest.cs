using System.Net;
using Day08_DTO_Mapper;
using Moq;
using Xunit;

namespace Day08_DTO_Mapper_Test.Reposetory;

public class PokemonRepositoryTest
{
    [Fact]
    public async Task 통신중_예외가_발생하면_재요청없이_전달한다()
    {
        var source = new Mock<IPokemonApiDataSource>(MockBehavior.Strict);
        var expected = new HttpRequestException("Connection failed");
        source.Setup(x => x.GetByNameAsync("ditto")).ThrowsAsync(expected);
        IPokemonRepository repository = new Repository(source.Object);

        var actual = await Assert.ThrowsAsync<HttpRequestException>(
            () => repository.GetPokemonByNameAsync("ditto"));

        Assert.Same(expected, actual);
        source.Verify(x => x.GetByNameAsync("ditto"), Times.Once);
        source.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task 포켓몬_정보가_없으면_null을_반환한다(bool byId)
    {
        var source = CreateSource(byId, new Response<PokemonDTO>(
            404, new Dictionary<string, string>(), null!));
        var repository = new Repository(source.Object);

        var pokemon = await GetPokemon(repository, byId);

        Assert.Null(pokemon);
        VerifyOnce(source, byId);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task 정상응답을_Model로_변환한다(bool byId)
    {
        var dto = new PokemonDTO
        {
            Id = 132, Name = "ditto",
            Types = new() { new TypeSlot { Type = new NamedApiResource { Name = "normal" } } },
            Stats = new() { new StatSlot { Stat = new NamedApiResource { Name = "hp" }, BaseStat = 48 } }
        };
        var source = CreateSource(byId, new Response<PokemonDTO>(
            200, new Dictionary<string, string>(), dto));
        var repository = new Repository(source.Object);

        var pokemon = await GetPokemon(repository, byId);

        Assert.NotNull(pokemon);
        Assert.Equal(132, pokemon.Id);
        Assert.Equal("ditto", pokemon.Name);
        Assert.Equal(new[] { "normal" }, pokemon.Types);
        Assert.Equal(48, pokemon.Stats["hp"]);
        Assert.Equal(string.Empty, pokemon.OfficialArtworkUrl);
        VerifyOnce(source, byId);
    }

    [Theory]
    [InlineData(false, 500)]
    [InlineData(true, 500)]
    [InlineData(false, 429)]
    [InlineData(true, 429)]
    public async Task 실패응답은_상태코드가_있는_예외로_전달한다(bool byId, int status)
    {
        var source = CreateSource(byId, new Response<PokemonDTO>(
            status, new Dictionary<string, string>(), null!));
        var repository = new Repository(source.Object);

        var error = await Assert.ThrowsAsync<HttpRequestException>(
            () => GetPokemon(repository, byId));

        Assert.Equal((HttpStatusCode)status, error.StatusCode);
        VerifyOnce(source, byId);
    }

    [Fact]
    public async Task 성공응답에_본문이_없으면_예외를_전달한다()
    {
        var source = CreateSource(false, new Response<PokemonDTO>(
            200, new Dictionary<string, string>(), null!));
        var repository = new Repository(source.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => repository.GetPokemonByNameAsync("ditto"));
        VerifyOnce(source, false);
    }

    private static Mock<IPokemonApiDataSource> CreateSource(
        bool byId, Response<PokemonDTO> response)
    {
        var source = new Mock<IPokemonApiDataSource>(MockBehavior.Strict);
        if (byId)
            source.Setup(x => x.GetByIdAsync(132)).ReturnsAsync(response);
        else
            source.Setup(x => x.GetByNameAsync("ditto")).ReturnsAsync(response);
        return source;
    }

    private static Task<Pokemon?> GetPokemon(IPokemonRepository repository, bool byId) =>
        byId ? repository.GetPokemonByIdAsync(132) : repository.GetPokemonByNameAsync("ditto");

    private static void VerifyOnce(Mock<IPokemonApiDataSource> source, bool byId)
    {
        if (byId)
            source.Verify(x => x.GetByIdAsync(132), Times.Once);
        else
            source.Verify(x => x.GetByNameAsync("ditto"), Times.Once);
        source.VerifyNoOtherCalls();
    }
}
