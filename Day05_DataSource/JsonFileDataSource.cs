namespace Day04_DataSource;
using System.Text.Json;

public class JsonFileDataSource : IDataSource
{
    private readonly List<Person> _people = new();
    private readonly string _filePath;

    public async Task<List<Person>> GetPeopleAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<Person>>(json)
               ?? new List<Person>();
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(people, options);
        await File.WriteAllTextAsync(_filePath, json);
    }
    public JsonFileDataSource(string filePath)
    {
        _filePath = filePath;
    }

}