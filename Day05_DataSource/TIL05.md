# Day 01 TIL

Day 05 TIL
이름: 서인호
작성일: <2026-09-07>
1. 오늘 막힌 부분 또는 내린 판단
   한번에 다 정리했었는데 인터페이스, 모델, 데이터소스 폴더로 다시 나눴어야했던점

2. 수정 전과 수정 후
   수정 전
   public class Person
   {
   public string Name { get; set; }
   public int Age { get; set; }
   }

public interface IDataSource
{
Task<List<Person>> GetPeopleAsync();
Task SavePeopleAsync(List<Person> people);
}

public class JsonFileDataSource : IDataSource
{
private readonly string _filePath;

    public JsonFileDataSource(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        if (!System.IO.File.Exists(_filePath))
            return new List<Person>();

        string json = await System.IO.File.ReadAllTextAsync(_filePath);
        return System.Text.Json.JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
    }

    public async Task SavePeopleAsync(List<Person> people)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(people, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        await System.IO.File.WriteAllTextAsync(_filePath, json);
    }
}
   수정 후 
class Program
{
      static async Task Main(string[] args)
 {
      IDataSource dataSource = new JsonFileDataSource("people.json");

        var people = await dataSource.GetPeopleAsync();
        var adults = people.Where(p => p.Age >= 19).ToList();
        Console.WriteLine("성인만 출력:");
        adults.ForEach(p => Console.WriteLine($"- {p.Name} ({p.Age}세)"));

        if (!people.Any(p => p.Name == "김민지"))
        {
            people.Add(new Person { Name = "김민지", Age = 15 });
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n김민지 추가 후 저장 완료.");
        }

        var toDelete = people.FirstOrDefault(p => p.Name == "이민준");
        if (toDelete != null)
        {
            people.Remove(toDelete);
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n이민준 삭제 후 저장 완료.");
        }
    }   
}



3. AI 사용 여부와 채택, 거절한 이유
   AI 사용 여부: <사용함 >
   질문: 1. 실행해도 아무것도 안나오는데 뭔가 더 만들어야하는지
        2. Program.cs를 실행했을떄 people.json을 참조하지 못하는것같아
   제안받은 내용: 1. people.json 작성
                2. Run/Debug의 Working Directory 수정
   

4. 검증 결과
   빌드: <성공>
   실행 결과: ![0907 1231.png](../../../Downloads/0907%201231.png)
   추가로 확인한 내용: 실행할때마다 이민준이 삭제되고 박민지가 추가되면서 수동으로 수정하지않으면 '삭제되엇다'는 말도 안나옴
5. 아직 궁금한 점 
   실행이 끝나고 원상태로 복구하는 기능도 있는지

6. 다음에 적용할 것