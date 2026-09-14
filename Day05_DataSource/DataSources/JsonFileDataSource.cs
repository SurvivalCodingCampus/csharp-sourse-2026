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
        if (File.Exists(path))
        {
            InitJsonTextAsync(Path).Wait();
        }
    }

    private readonly JsonSerializerOptions _option = new JsonSerializerOptions{
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /*
    Async 사용하기 전
    public Task<List<Person>> GetPeopleAsync()
    {
        try
        {
            List<Person>? people = JsonSerializer.Deserialize<List<Person>>(
                File.ReadAllText(Path),
                _option
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
    */

    public async Task<List<Person>> GetPeopleAsync()
    {
        await using Stream json = File.OpenRead(Path);
        var people = await JsonSerializer.DeserializeAsync<List<Person>>(json, _option);
        if (people is null) throw new Exception("Path not found");

        return await Task.FromResult(people);
    }

    public async Task SavePeopleAsync(List<Person> people)
    {   
        string jsonString = JsonSerializer.Serialize(people, _option);
        await File.WriteAllTextAsync(Path,jsonString, Encoding.UTF8);
    }

    // 테스트용 Json파일 리스트 초기화
    public async Task InitJsonTextAsync(string path)
    {
        if (File.Exists("DefaultPeople.json"))
        {
            await File.Create("DefaultPeople.json").DisposeAsync();
        }
        string? jsonString = await File.ReadAllTextAsync("DefaultPeople.json");
        
        await File.WriteAllTextAsync(path, jsonString, Encoding.UTF8);
    }
}