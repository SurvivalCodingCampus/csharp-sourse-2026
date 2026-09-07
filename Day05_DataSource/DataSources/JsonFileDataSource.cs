using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Day05_DataSource;

public class JsonFileDataSource : IDataSource
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

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

        var json = await File.ReadAllTextAsync(_filePath);
        var people = JsonSerializer.Deserialize<List<Person>>(json);
        return people ?? new List<Person>();
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        var json = JsonSerializer.Serialize(people, SerializerOptions);
        await File.WriteAllTextAsync(_filePath, json);
    }
}
