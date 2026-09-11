using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day06_OOP_Model_N_Repository.DataSources;

public class JsonFileItemDataSource : IItemDataSource {
    public string jsonFilePath;
    public JsonFileItemDataSource(string jsonFilePathSave) {
        this.jsonFilePath = jsonFilePathSave;
    }

    public async Task<List<Item>> LoadAllItemsAsync() {
        try {
            string jsonFileToLoad = await File.ReadAllTextAsync(jsonFilePath); // 실행 
            var jsonFileToSave = JsonSerializer.Deserialize<List<Item>>(jsonFileToLoad);// 실행
                                                                                        // 동시 출발하지만 await 때문에 파일 읽기가 완료될 때까지 기다린 후, 다음 줄을 실행함
                                                                                        // 즉 async(비동기)를 await로 하여금 📌⭐동기화처럼 보이게 함
            return jsonFileToSave;
        }
        catch (FileNotFoundException) {
            return new List<Item>(); // 원래 타입을 반환
        }
    }

    public async Task SaveAllItemAsync(List<Item> items) {
        await File.WriteAllTextAsync(jsonFilePath, JsonSerializer.Serialize(items));  //await했더니 자동 요약됨..
    }
}