using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

// Person 데이터 모델 클래스
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// IDataSource 인터페이스
public interface IDataSource
{
    Task<List<Person>> GetPeopleAsync();
    Task SavePeopleAsync(List<Person> people);
}

// JsonFileDataSource 구현 클래스
public class JsonFileDataSource : IDataSource
{
    private readonly string _filePath;

    public JsonFileDataSource(string filePath)
    {
        _filePath = filePath;
    }

    // 비동기로 JSON 파일에서 데이터를 읽어오는 메서드
    public async Task<List<Person>> GetPeopleAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        string jsonString = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Person>>(jsonString) ?? new List<Person>();
    }

    // 비동기로 데이터를 JSON 파일에 저장하는 메서드
    public async Task SavePeopleAsync(List<Person> people)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(people, options);
        await File.WriteAllTextAsync(_filePath, jsonString);
    }
}