using System.Collections.Generic;
using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Mapper;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day08_DTO_Mapper.Tests.Data.Mapper;

[TestClass]
public class PokemonMapperTests
{
    [TestMethod]
    public void ToDomain_ValidDto_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Id = 25,
            Name = "pikachu",
            Height = 4,
            Weight = 60,
            Sprites = new PokemonSpritesDto { FrontDefault = "https://example.com/pikachu.png" },
            Types = new List<PokemonTypeSlotDto>
            {
                new() { Slot = 1, Type = new NamedApiResourceDto { Name = "electric" } }
            }
        };

        // Act
        var result = dto.ToDomain();

        // Assert
        Assert.AreEqual(25, result.Id);
        Assert.AreEqual("pikachu", result.Name);
        Assert.AreEqual(0.4, result.HeightMeter);
        Assert.AreEqual(6.0, result.WeightKg);
        Assert.AreEqual("https://example.com/pikachu.png", result.ImageUrl);
        Assert.AreEqual(1, result.Types.Count);
        Assert.AreEqual("electric", result.Types[0]);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ToDomain_InvalidOrEmptyName_SetsDefaultNameToUnknown(string? invalidName)
    {
        // Arrange
        var dto = new PokemonDto
        {
            Id = 1,
            Name = invalidName!,
            Height = 10,
            Weight = 100
        };

        // Act
        var result = dto.ToDomain();

        // Assert
        Assert.AreEqual("Unknown", result.Name);
    }

    [TestMethod]
    public void ToDomain_NegativeNumbers_SetsToZero()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Id = -10,
            Name = "charmander",
            Height = -5,
            Weight = -50
        };

        // Act
        var result = dto.ToDomain();

        // Assert
        Assert.AreEqual(0, result.Id);
        Assert.AreEqual(0, result.HeightDecimeter);
        Assert.AreEqual(0.0, result.HeightMeter);
        Assert.AreEqual(0, result.WeightHectogram);
        Assert.AreEqual(0.0, result.WeightKg);
    }

    [TestMethod]
    public void ToDomain_NullDto_ReturnsDefaultPokemonWithoutThrowingException()
    {
        // Arrange
        PokemonDto? nullDto = null;

        // Act & Assert (어떠한 예외도 던지지 않고 기본 객체 반환)
        var result = nullDto.ToDomain();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Id);
        Assert.AreEqual("Unknown", result.Name);
        Assert.AreEqual(0, result.Types.Count);
    }

    [TestMethod]
    public void ToDomain_CorruptedTypesAndSprites_HandlesGracefully()
    {
        // Arrange: 하위 객체가 null이거나 깨져 있는 비정상 DTO
        var dto = new PokemonDto
        {
            Id = 132,
            Name = "ditto",
            Sprites = null!, // Sprites 자체가 null
            Types = new List<PokemonTypeSlotDto>
            {
                new() { Slot = 1, Type = null! }, // Type이 null인 슬롯
                new() { Slot = 2, Type = new NamedApiResourceDto { Name = "   " } }, // 공백 이름
                new() { Slot = 3, Type = new NamedApiResourceDto { Name = "normal" } }
            }
        };

        // Act
        var result = dto.ToDomain();

        // Assert
        Assert.IsNull(result.ImageUrl);
        Assert.AreEqual(1, result.Types.Count);
        Assert.AreEqual("normal", result.Types[0]);
    }
}