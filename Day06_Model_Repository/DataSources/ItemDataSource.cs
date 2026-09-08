using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Day06_Model_Repository.Interfaces;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.DataSources;

public class ItemDataSource(string path = "ItemList.json") : IItemDataSource
{
    private string Path { get; set; } = path;
    
    // 한글 깨짐 방지 옵션
    private readonly JsonSerializerOptions _option = new JsonSerializerOptions{
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    
    // 아이템 데이터를 읽어옴
    public async Task<List<Item>> LoadAllItemsAsync()
    {
        // 저장된 파일이 없을 경우 빈 파일 생성
        if (!File.Exists(Path))
        {
            await File.WriteAllTextAsync(Path, "[]");
        
            var items = JsonSerializer.Deserialize<List<Item>>(Path, _option);
            if (items is null)
            {
                throw new  Exception("Path not found");
            }

            return await Task.FromResult(items);
        }

        await using Stream stream = File.OpenRead(Path);
        var itemList =  await JsonSerializer.DeserializeAsync<List<Item>>(stream, _option);

        return itemList ?? throw new Exception("Path not found");


    }

    // 아이템 데이터를 저장함
    public async Task SaveAllItemsAsync(List<Item> items)
    {
        string jsonString = JsonSerializer.Serialize(items, _option);
        await File.WriteAllTextAsync(Path,jsonString, Encoding.UTF8);
    }
}