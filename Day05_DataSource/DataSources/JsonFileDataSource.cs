using System.Text.Encodings.Web;
using System.Text.Json;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class JsonFileDataSource : IDataSource
{
    private readonly string filePath;

    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public JsonFileDataSource(string filePath)
    {
        this.filePath = filePath;
    }

    public Task<List<Person>> GetPeopleAsync()
    {
        try
        {
            List<Person>? people = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(filePath), options);

            if (people is null)
            {
                throw new Exception("People not found");
            }

            return Task.FromResult(people);
        }
        catch (FileNotFoundException e)
        {
            // 서버에서 에러 로그 수집
            throw new Exception("People not found");
        }
    }

    public async Task SavePeopleAsync(List<Person> person)
    {
        string jsonString = JsonSerializer.Serialize(person, options);
        await File.WriteAllTextAsync(filePath, jsonString);
    }
}