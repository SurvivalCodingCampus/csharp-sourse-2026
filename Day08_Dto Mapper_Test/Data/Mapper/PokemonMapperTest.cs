using System.Text.Json;
using Day07_http_WithAI.Data.DTOs;
using Day07_http_WithAI.Data.Mapper;
using Day07_http_WithAI.Models;
using Xunit;
using Assert = Xunit.Assert;


namespace Day08_Dto_Mapper_Test.Data.Mapper;


public class PokemonMapperTest
{
    [Fact]
    public void 정상적인_데이터를_Pokemon으로_변환한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "pikachu",
            Sprites = new SpritesDto
            {
                FrontDefault = "https://example.com/pikachu.png"
            }
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("pikachu", result.Name);
        Assert.Equal(
            "https://example.com/pikachu.png",
            result.ImageUrl);
    }

    [Fact]
    public void Name이_null이면_NO_NAME을_사용한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = null
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("NO NAME", result.Name);
    }

    [Fact]
    public void Name이_빈문자열이면_NO_NAME을_사용한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = ""
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("NO NAME", result.Name);
    }

    [Fact]
    public void Name이_공백이면_NO_NAME을_사용한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "   "
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("NO NAME", result.Name);
    }

    [Fact]
    public void Name의_앞뒤_공백을_제거한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "  pikachu  "
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("pikachu", result.Name);
    }

    [Fact]
    public void Sprites가_null이면_ImageUrl을_빈문자열로_처리한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "pikachu",
            Sprites = null
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("pikachu", result.Name);
        Assert.Equal("", result.ImageUrl);
    }

    [Fact]
    public void FrontDefault가_null이면_ImageUrl을_빈문자열로_처리한다()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "pikachu",
            Sprites = new SpritesDto
            {
                FrontDefault = null
            }
        };

        // Act
        Pokemon result = dto.ToModel();

        // Assert
        Assert.Equal("", result.ImageUrl);
    }
    
    //AI: dto == null일 때 상황 보완
    
    // [Fact]
    // public void DTO가_null이면_기본_Pokemon을_반환한다()
    // {
    //     // Arrange
    //     PokemonDto? dto = null;
    //
    //     // Act
    //     Pokemon result = dto.ToModel();
    //
    //     // Assert
    //     Assert.Equal("NO NAME", result.Name);
    //     Assert.Equal("", result.ImageUrl);
    // }
}