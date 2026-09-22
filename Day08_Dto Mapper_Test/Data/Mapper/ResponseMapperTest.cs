using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Day07_http_WithAI.Data.Mapper;
using Xunit;

namespace Day08_Dto_Mapper_Test.Data.Mapper;


public class ResponseMapperTest
{

    [Fact]
    public async Task 정상적인_HTTP_Response를_Response로_변환한다()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Hello World")
        };

        // Act
        var result = await response.ToResponse();

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Hello World", result.Body);
    }

    [Fact]
    public async Task StatusCode_404를_정상적으로_변환한다()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("Not Found")
        };

        // Act
        var result = await response.ToResponse();

        // Assert
        Assert.Equal(404, result.StatusCode);
        Assert.Equal("Not Found", result.Body);
    }

    [Fact]
    public async Task Header를_정상적으로_변환한다()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Hello")
        };

        response.Headers.Add("X-Test", "TestValue");

        // Act
        var result = await response.ToResponse();

        // Assert
        Assert.True(result.Headers.ContainsKey("X-Test"));
        Assert.Equal("TestValue", result.Headers["X-Test"]);
    }

    [Fact]
    public async Task 여러_개의_Header를_정상적으로_변환한다()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Hello")
        };

        response.Headers.Add("X-Test", "TestValue");
        response.Headers.Add("X-Number", "123");

        // Act
        var result = await response.ToResponse();

        // Assert
        Assert.Equal("TestValue", result.Headers["X-Test"]);
        Assert.Equal("123", result.Headers["X-Number"]);
    }

    [Fact]
    public async Task Body가_빈문자열이면_빈문자열로_변환한다()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("")
        };

        // Act
        var result = await response.ToResponse();

        // Assert
        Assert.Equal("", result.Body);
    }
}