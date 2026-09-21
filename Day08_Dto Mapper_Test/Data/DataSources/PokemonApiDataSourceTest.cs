using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Day07_http_WithAI.Data.DataSources;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Xunit;
using Assert = Xunit.Assert;

namespace Day08_Dto_Mapper_Test.Data.DataSources;


public class PokemonApiDataSourceTest
{
    //과제4
    
    [Fact]
    public async Task GetPokemonAsync_Pikachu_ReturnsSuccessResponse()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(
                "{\"name\":\"pikachu\",\"id\":25}"
            )
        };

        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(mockHandler.Object);

        var dataSource = new PokemonApiDataSource(httpClient);


        // Act
        Response response =
            await dataSource.GetPokemonAsync("pikachu");


        // Assert
        Assert.Equal(200, response.StatusCode);
        Assert.NotNull(response.Body);
        Assert.Contains("pikachu", response.Body);
    }


    [Fact]
    public async Task GetPokemonAsync_InvalidPokemon_Returns404()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent(
                "{\"detail\":\"Not found.\"}"
            )
        };

        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(mockHandler.Object);

        var dataSource = new PokemonApiDataSource(httpClient);


        // Act
        Response response =
            await dataSource.GetPokemonAsync("does-not-exist");


        // Assert
        Assert.Equal(404, response.StatusCode);
    }


    [Fact]
    public async Task GetPokemonAsync_Pikachu_CallsHttpRequestOnce()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(
                "{\"name\":\"pikachu\",\"id\":25}"
            )
        };

        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(mockHandler.Object);

        var dataSource = new PokemonApiDataSource(httpClient);


        // Act
        await dataSource.GetPokemonAsync("pikachu");


        // Assert
        mockHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}