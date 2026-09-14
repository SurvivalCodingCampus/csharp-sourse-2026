using System.Text.Json;

namespace Day06_Repository;

public class JsonFileItemDataSource : IItemDataSource
{
    private readonly string _filePath;

    public JsonFileItemDataSource(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<Item>> LoadAllItemsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Item>();
        }

        await using var stream = File.OpenRead(_filePath);
        var items = await JsonSerializer.DeserializeAsync<List<Item>>(stream);
        return items ?? new List<Item>();
    }

    public async Task SaveAllItemsAsync(List<Item> items)
    {
        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, items, new JsonSerializerOptions { WriteIndented = true });
    }
}
