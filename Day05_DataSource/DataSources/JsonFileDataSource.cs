using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class JsonFileDataSource : IDataSource
{
    private string Path { get; set; }
    public JsonFileDataSource(string path)
    {
        Path = path;
        InitJsonTextAsync(Path).Wait();
    }

    private JsonSerializerOptions option = new JsonSerializerOptions{
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    
    public Task<List<Person>> GetPeopleAsync()
    {
        try
        {
            List<Person>? people = JsonSerializer.Deserialize<List<Person>>(
                json : File.ReadAllText(Path),
                option
            );
            if (people is null) throw new Exception("Path not found");

            return Task.FromResult(people);

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        string jsonString = JsonSerializer.Serialize(people, option);
        await File.WriteAllTextAsync("people.json",jsonString, Encoding.UTF8);
    }

    // 테스트용 Json파일 리스트 초기화
    public async Task InitJsonTextAsync(string path)
    {
        string jsonString = await File.ReadAllTextAsync("DefaultPeople.json");
        await File.WriteAllTextAsync(path, jsonString, Encoding.UTF8);
    }
}