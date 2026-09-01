# Day 03 TIL

- 이름: 장종민
- 작성일: 2026-08-31

## 1. 오늘 막힌 부분 또는 내린 판단

- JSON 직렬화 후 한글이 `\uCD1D..` 형태로 저장되는 현상을 확인
- 한글로 저장하기 위해 `JsonSerializerOptions`의 Encoder 설정을 사용

## 2. 수정 전과 수정 후

### 수정 전

```csharp
// DefaultFileCopier.cs
public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}
```

### 수정 후

```csharp
// DefaultFileCopier.cs
public class DefaultFileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        string text = File.ReadAllText(sourceFilePath);
        File.AppendAllText(destinationFilePath, text);
    }
}

// Copy Main
public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}

class Program
{
    static void Main(string[] args)
    {
        DefaultFileCopier copier = new DefaultFileCopier();
        copier.CopyFile(args[0], args[1]);
    }
}

// JSON 직렬화 코드 추가
class Program
{
    static void Main(string[] args)
    {
        Department department = new Department("총무부", new Employee("홍길동", 41));

        // 이거 안쓰면 한글 깨짐
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        string json = JsonSerializer.Serialize(department, options);

        File.WriteAllText("company.json", json);
        
        
        string jsonText = File.ReadAllText("company.json");

        Department? result = JsonSerializer.Deserialize<Department>(jsonText);

        if (result != null)
        {
            Console.WriteLine(result.Name);
            Console.WriteLine(result.leader.Name);
            Console.WriteLine(result.leader.Age);
        }
    }
}
```

`copier.CopyFile(args[0], args[1]);`
전달한 값에 따라 다른 파일을 사용

`JavaScriptEncoder.UnsafeRelaxedJsonEscaping`을 사용하여 한글이 
유니코드 이스케이프 형태가 아닌 실제 한글로 저장
`WriteIndented = true`를 사용하여 JSON을 줄바꿈과 들여쓰기가 
적용된 형태로 저장해 사람이 읽기 쉽게 만듦

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용함
- 질문: 이대로 작성하니 JSON 파일에서 한글이 깨진다.
- 제안받은 내용: `Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping` 코드를 추가하면 한글이 나옴
- 채택 또는 거절한 내용: 채택
- 판단한 이유: 한글이 안깨지는 걸 확인했으며, 추가적으로 `WriteIndented = true` 를 넣으면 
             JSON을 사람이 좋게 줄바꿈하고 들여쓰기를 한다고 정보제공

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: `{
              "Name": "총무부",
              "leader": {
                  "Name": "홍길동",
                  "Age": 41
              }
            }`
- 추가로 확인한 내용: 테스크코드에서 역직렬화 값도 같은지 확인

## 5. 아직 궁금한 점

파일 경로를 실행 시 전달받기 위해 `args[0], args[1]`을 사용
하지만 테스트에서는 `Main()`이 실행되지 않아 테스트 값을 따로 작성해야 했고,
이 방식이 맞는지 아직 확신이 없는 상태


## 6. 다음에 적용할 것

5번에서 확인한 방식이 맞다면, `Main()`의 실행 방식과 테스트 코드는 별개로 생각하고
테스트에 필요한 값은 테스트 코드에서 직접 준비해서 작성
