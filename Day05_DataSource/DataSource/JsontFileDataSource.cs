using System.Text.Json;
using Day05_DataSource.Interface;
using Day05_DataSource.Model;

namespace Day05_DataSource.DataSource;

public class JsontFileDataSource: IDataSource { 
    //1. 데이터를 불러와 필터링
    private readonly string Name;

    // 메서드의 생성자
    public JsontFileDataSource(string name) {
        this.Name = name;
    }

    public async Task<List<Person>> GetPeopleAsync() { // IPersonDataSource > GetPeopleAsync
        
        //파일 읽기위해 변수를 지정하여 파일 불러옴
        try {
            string getF = File.ReadAllText(Name);
            //읽을때는 역직렬
            var setF = JsonSerializer.Deserialize<List<Person>>(getF);
            return setF;
        }
        catch (FileNotFoundException e) {
            Console.WriteLine("파일 찾을 수 없어");
            return null;
        } 
    }

    public async Task SavePeopleAsync(List<Person> people) {
        await Task.Delay((500)); 
        string writeSetF = JsonSerializer.Serialize<List<Person>>(people);
        await File.WriteAllTextAsync(Name, writeSetF);

    }

}