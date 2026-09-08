using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class FilePersonDataSource : IPersonDataSource
{
    private JsonSerializerOptions option = new JsonSerializerOptions{
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    
    public Task<Person> GetPerson(string Name)
    {
        try
        {
            Person? person = JsonSerializer.Deserialize<Person>(
                json : File.ReadAllText("person.json"),
                option
            );
            if (person is null) throw new Exception("Person not found");
        
            return Task.FromResult(person);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task SavePerson(Person person)
    {
        string jsonString = JsonSerializer.Serialize(person, option);
        await File.WriteAllTextAsync("person.json",jsonString, Encoding.UTF8);
    }
}