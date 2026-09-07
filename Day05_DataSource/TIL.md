# Day 05 TIL

- 이름: 장종민
- 작성일: 2026-09-07

## 1. 오늘 막힌 부분 또는 내린 판단

JSON 파일에 여러 명의 데이터가 들어 있는데  
Person 한 명으로 역직렬화하려고 하여 오류가 발생

## 2. 수정 전과 수정 후

### 수정 전

```csharp
Program.cs

using Day05_DataSource.DataSources;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource;

class Program
{
    static async Task Main(string[] args)
    {
        IPersonDataSources dataSources = new FilePersonDataSources();

        // 1. 데이터 불러오기
        Person person = await dataSources.GetPerson("오준석");

        // UI
        Console.WriteLine(person);

        // 2. 데이터 저장하기
        Person newPerson = new Person(name: "홍길동", age: 10);
        await dataSources.SavePerson(newPerson);
    }
}

Person.cs

namespace Day05_DataSource.Models;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Person()
    {
    }
}

IPersonDataSources.cs

using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IPersonDataSources
{
    Task<Person> GetPerson(string name);
    Task SavePerson(Person person);
}

FilePersonDataSources.cs

using System.Text.Encodings.Web;
using System.Text.Json;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class FilePersonDataSources : IPersonDataSources
{
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public Task<Person> GetPerson(string name)
    {
        try
        {
            Person? person = JsonSerializer.Deserialize<Person?>(
                File.ReadAllText("person.json"),
                options
            );

            if (person is null)
            {
                throw new Exception("Person not found");
            }

            return Task.FromResult(person);
        }
        catch (FileNotFoundException)
        {
            // 서버에서 에러 로그 수집
            throw new Exception("Person not found");
        }
    }

    public async Task SavePerson(Person person)
    {
        string jsonString = JsonSerializer.Serialize(person, options);
        await File.WriteAllTextAsync("person.json", jsonString);
    }
}

```

### 수정 후

```csharp

Program.cs

using Day05_DataSource.DataSources;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;


namespace Day05_DataSource;

class Program
{
    static async Task Main(string[] args)
    {
        IDataSource dataSource = new JsonFileDataSource("people.json");

        // 1. 데이터를 불러와 필터링하는 로직
        var people = await dataSource.GetPeopleAsync();
        var adults = people.Where(p => p.Age >= 19).ToList();
        Console.WriteLine("성인만 출력:");
        adults.ForEach(p => Console.WriteLine($"- {p.Name} ({p.Age})세"));

        // 2. 데이터를 추가하고 저장하는 로직
        people.Add(new Person { Name = "김민지", Age = 15 });
        await dataSource.SavePeopleAsync(people);
        Console.WriteLine("\n김민지 추가 후 저장 완료.");

        // 3. 데이터를 삭제하는 로직
        var toDelete = people.FirstOrDefault(p => p.Name == "이민준");
        if (toDelete != null)
        {
            people.Remove(toDelete);
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n이민준 삭제 후 저장 완료.");
        }
    }
}

Person.cs

using Day05_DataSource.DataSources;
using Day05_DataSource.Interfaces;

namespace Day05_DataSource.Models;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Person()
    {
    }
}

IDataSource.cs

using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IDataSource
{
    Task<List<Person>> GetPeopleAsync();
    Task SavePeopleAsync(List<Person> people);
}

JsonFileDataSource.cs

using System.Text.Encodings.Web;
using System.Text.Json;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource.DataSources;

public class JsonFileDataSource : IDataSource
{
    private readonly string filePath;

    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public JsonFileDataSource(string filePath)
    {
        this.filePath = filePath;
    }

    public Task<List<Person>> GetPeopleAsync()
    {
        try
        {
            List<Person>? people = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(filePath), options);

            if (people is null)
            {
                throw new Exception("People not found");
            }

            return Task.FromResult(people);
        }
        catch (FileNotFoundException e)
        {
            // 서버에서 에러 로그 수집
            throw new Exception("People not found");
        }
    }

    public async Task SavePeopleAsync(List<Person> person)
    {
        string jsonString = JsonSerializer.Serialize(person, options);
        await File.WriteAllTextAsync(filePath, jsonString);
    }
}
```

1. 과제에 맞게 클래스, 인터페이스, 메서드 이름 변경  
ex) `IPersonDataSources` -> `IDataSource`  
    `GetPerson ()` -> `GetPeopleAsync ()`

2. 한 명만 불러오고 저장을 List로 여러 명 관리  
ex) `<Person>` -> `List<Person>`

3. 생성자를 통해 JSON 파일 경로를 전달받도록 변경
4. `WriteIndented = true` 를 추가하여 JSON 들여쓰기 정렬

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용함
- 질문: 여러 명을 반환하는 과정에서 오류 발생
- 제안받은 내용: `List<T>` 를 사용
- 채택 또는 거절한 내용: `<Person>` -> `List<Person>` 이로 수정
- 판단한 이유: JSON 파일이 여러 개의 Person 객체를 가진 배열 형태이므로 Person 한 개로는 역직렬화할 수 없고,  
             List<Person>으로 받아야 하기 때문

## 4. 검증 결과

- 빌드: 성공
- 실행 결과:
  성인만 출력:
- 홍길동 (30)세
- 이민준 (20)세

김민지 추가 후 저장 완료.

이민준 삭제 후 저장 완료.

- 추가로 확인한 내용: 김민지가 `people.json`에 정상적으로 추가되는지 확인  
                   JSON 파일이 들여쓰기된 형태로 저장되는지 확인

## 5. 아직 궁금한 점

`List<Person>` 이외의 컬렉션을 사용해서도 같은 기능을 구현할 수 있는지

## 6. 다음에 적용할 것

JSON에서 여러 개의 데이터를 처리할 때 JSON의 형태를 먼저 확인   
배열 형태라면 List<T> 등의 컬렉션을 사용해서 역직렬화하겠다.