using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class FilePersonDataSources : IPersonDataSources
{
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public Task<Person> GetPerson(string name)
    {
        try
        {
            Person? person = JsonSerializer.Deserialize<Person?>(File.ReadAllText("person.json"), options);

            if (person is null)
            {
                throw new Exception("Person not found");
            }

            return Task.FromResult(person);
        }
        catch (FileNotFoundException e)
        {
            // 서버에서 에러 로그 수집
            throw new Exception("Person not found");
        }
    }

    public async Task SavePerson(Models.Person person)
    {
        string jsonString = JsonSerializer.Serialize(person, options);
        await File.WriteAllTextAsync("person.json", jsonString);
    }
}