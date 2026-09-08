using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Day04_DataSource;
using Xunit;


using Day05_DataSource;

namespace Day05_DataSource.Tests;

public class JsonFileDataSourceTests : IDisposable
{
    private readonly string _filePath =
        Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

    public void Dispose()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }

    [Fact]
    public async Task People()
    {
        // Arrange
        IDataSource dataSource = new JsonFileDataSource(_filePath);

        var expected = new List<Person>
        {
            new("김민지", 15),
            new("이민준", 25)
        };

        // Act
        await dataSource.SavePeopleAsync(expected);
        var actual = await dataSource.GetPeopleAsync();

        // Assert
        Assert.Equal(2, actual.Count);
        Assert.Equal("김민지", actual[0].Name);
        Assert.Equal(15, actual[0].Age);
        Assert.Equal("이민준", actual[1].Name);
        Assert.Equal(25, actual[1].Age);
    }

    [Fact]
    public async Task ReturnEmptyFile()
    {
        // Arrange
        IDataSource dataSource = new JsonFileDataSource(_filePath);

        // Act
        var people = await dataSource.GetPeopleAsync();

        // Assert
        Assert.NotNull(people);
        Assert.Empty(people);
    }

    [Fact]
    public async Task SaveEmptyJsonFile()
    {
        // Arrange
        IDataSource dataSource = new JsonFileDataSource(_filePath);

        // Act
        await dataSource.SavePeopleAsync(new List<Person>());
        var people = await dataSource.GetPeopleAsync();

        // Assert
        Assert.Empty(people);
    }
}