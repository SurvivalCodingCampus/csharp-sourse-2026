using System.Text.Json;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class JsonFileDataSource : IUserDataSource
{
    private readonly string _filePath;

    public JsonFileDataSource(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Person>();
        }

        return JsonSerializer.Deserialize<List<Person>>(json)
               ?? new List<Person>();
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        string json = JsonSerializer.Serialize(
            people,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        await File.WriteAllTextAsync(_filePath, json);
    }
}