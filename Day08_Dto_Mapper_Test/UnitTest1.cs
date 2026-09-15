namespace Day08_Dto_Mapper_Test;
using System.Threading.Tasks;

public class PokemonApiDataSourceTests {

    [Test]
    public async Task GetPokemonAsync_ditto조회시_200OK응답과_JSON반환()
    {
        // Given = Arrange (준비)
        var dataSource = new MockPokemonApiDataSource2();

        // when = Act (실행)
        var response = await dataSource.GetPokemonAsync("ditto");

        // Then = Assert (검증)
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(200));
        Assert.That(response.Body, Does.Contain("ditto"));
        Assert.That(response.Body, Does.Contain("\"id\":132"));
    }

    [Test]
    public async Task GetPokemonAsync_unknown조회시_404응답반환() {
        // Arrange (준비)
        var dataSource = new MockPokemonApiDataSource2();

        // Act (실행)
        var response = await dataSource.GetPokemonAsync("unknown");

        // Assert (검증)
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(404));
        Assert.That(response.Body, Is.EqualTo("Not found"));
    }

    [Test]
    public async Task GetPokemonAsync_지원하지않는이름조회시_ArgumentException발생() {
        // Arrange (준비)
        var dataSource = new MockPokemonApiDataSource2();

        // Act & Assert (실행 및 예외 검증)
        Assert.ThrowsAsync<ArgumentException>(async () => { await dataSource.GetPokemonAsync("pikachu"); });
    }
}