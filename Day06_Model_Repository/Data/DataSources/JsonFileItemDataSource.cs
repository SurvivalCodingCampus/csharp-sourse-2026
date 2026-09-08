using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.DataSources;

public class JsonFileItemDataSource : IItemDataSource
{
    private readonly string _fileName;

    public JsonFileItemDataSource(string fileName = "inventory.json")
    {
        _fileName = fileName;
    }

    private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public async Task<List<Item>> LoadAllItemsAsync()
    {
        // 파일이 아직 없으면 -> 빈 목록으로 시작 (첫 실행 대비)
        if (!File.Exists(_fileName))
        {
            return new List<Item>();
        }

        using FileStream openStream = File.OpenRead(_fileName);
        var item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream, _jsonSerializerOptions);
        return item ?? new List<Item>();
    }

    public async Task SaveAllItemsAsync(List<Item> items)
    {
        using FileStream openStream = File.OpenWrite(_fileName);
        await JsonSerializer.SerializeAsync(openStream, items, _jsonSerializerOptions);
    }
}