using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class FilePersonDataSource : IPersonDataSource
{
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    
    public Task<Person> GetPerson(string name)
    {
        File.Open("person.json", FileMode.OpenOrCreate);
        
        try
        {
            Person? person = JsonSerializer.Deserialize<Person>(
                File.ReadAllText("person.json"),
                options
            );

            if (person is null)
            {
                throw new Exception("Person not found");
            }

            return Task.FromResult(person);
        }
        catch (FileNotFoundException e)
        {
            // 서벗에서 에러 로그 수집 
            throw new Exception("Person not found");
        }
    }

    public async Task SavePerson(Person person)
    {
        string jsonString = JsonSerializer.Serialize(person, options);
        await File.WriteAllTextAsync("person.json", jsonString);
    }
}