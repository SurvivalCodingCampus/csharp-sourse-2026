using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class JsonFileDataSource : IDataSource
{
    private readonly string _fileName;

    public JsonFileDataSource(string fileName)
    {
        _fileName = fileName;
    }

    public async Task<List<People>> GetPeopleAsync()
    {
        string jsonString = await File.ReadAllTextAsync(_fileName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // 대소문자 무시 여부
            NumberHandling = JsonNumberHandling.AllowReadingFromString  // 문자열 형태의 숫자를 숫자로 허용
        };
        var people = JsonSerializer.Deserialize<List<People>>(jsonString, options);
        return people ?? new List<People>();
    }

    public async Task SavePeopleAsync(List<People> people)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,   // 들여쓰기 여부
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)   // 유니코드 한글
        };
        string jsonString = JsonSerializer.Serialize(people, options);
        await File.WriteAllTextAsync(_fileName, jsonString);
    }
}