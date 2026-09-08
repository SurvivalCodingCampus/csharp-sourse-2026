using System.Text.Encodings.Web;
using System.Text.Json;

namespace Day05_DataSource;

public class JsonFileDataSource : IDataSoure
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;
    
    public JsonFileDataSource(string filePath)
    {
        _filePath = filePath;
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        using var stream = File.OpenRead(_filePath);
        var people = await JsonSerializer.DeserializeAsync<List<Person>>(stream, _jsonOptions);
        return people ?? new List<Person>();
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, people, _jsonOptions);
    }
}