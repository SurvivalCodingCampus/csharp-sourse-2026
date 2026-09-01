# Day 04 TIL

- 이름: <이혁>
- 작성일: <2026-09-01>

## 1. 오늘 막힌 부분 또는 내린 판단

<개념은 이해가 잘 되지만, 개념을 활용해서 바로 코드를 작성하기 위해서 많은 시도와 고민을 해도 여전히 어려워서 AI의 도움을 많이 받았습니다.>

## 2. 수정 전과 수정 후

### 수정 전

```csharp
namespace Day04_Asynchronous;

using System;

public class Birds
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("새소리 시작");

        Task bird1 = MakeSoundAsync("꾸우", 1000); 
        Task bird2 = MakeSoundAsync("까악", 2000); 
        Task bird3 = MakeSoundAsync("짹짹", 3000); 

        await Task.WhenAll(bird1, bird2, bird3);

        Console.WriteLine("모든 새소리가 끝났습니다. 프로그램을 종료합니다.");
    }
}
```

### 수정 후

```csharp
namespace Day04_Asynchronous;

using System;
using System.Threading.Tasks;

public class Birds
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("새소리 시작");

        Task bird1 = MakeSoundAsync("꾸우", 1000, 4); 
        Task bird2 = MakeSoundAsync("까악", 2000, 4); 
        Task bird3 = MakeSoundAsync("짹짹", 3000, 4); 

        await Task.WhenAll(bird1, bird2, bird3);

        Console.WriteLine("모든 새소리가 끝났습니다. 프로그램을 종료합니다.");
    }

    static async Task MakeSoundAsync(string sound, int intervalMs, int times)
    {
        for (int i = 0; i < times; i++)
        {
            await Task.Delay(intervalMs);
            Console.WriteLine($"{sound} ({DateTime.Now:HH:mm:ss})");
        }
    }
}
```

<Run을 실행해보니 실행 도구 창에 원하는 출력값이 안 나와 AI한테 여러 번 질문하여 수정하게 되었습니다.>

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: <사용함>
- 질문: <내가 작성한 코드의 문제점이 뭐야?>
- 제안받은 내용: <새소리를 실행하는 for문이 빠졌습니다.>
- 채택 또는 거절한 내용: <for문을 추가하였습니다.>
- 판단한 이유: <코드를 살펴보니, 새소리를 실행하는 코드가 없어서 for문을 추가하여 Run 해보니 알맞은 출력값이 나와 코드를 추가하였습니다.>

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: <성공>
- 실행 결과: <실행 도구 창으로 실행 결과 확인 후, 알맞은 출력값이 나와 Git Hub에 사진으로 첨부하였습니다.>
- 추가로 확인한 내용:

## 5. 아직 궁금한 점

<위의 코드는 try-catch로 테스트 할 수 없다고 하셨는데 다른 테스트할 수 있는 방법이 있는지 찾아봐야겠습니다.>

## 6. 다음에 적용할 것

<using System.Threading.Tasks;는 Task와 관련된 클래스들이 들어있는 네임스페이스인 것을 기억하고 다음에 적용해야 하는 시기에 적용하겠습니다.>