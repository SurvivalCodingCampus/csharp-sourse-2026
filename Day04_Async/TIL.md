# Day 04 TIL

- 이름: 장종민
- 작성일: 2026-09-04

## 1. 오늘 막힌 부분 또는 내린 판단

각 새의 동작을 별도의 비동기 함수로 만들고, 
세 함수를 동시에 실행한 뒤 `Task.WhenAll()`을 사용해서
모든 작업이 끝날 때까지 기다리도록 구현


## 2. 수정 전과 수정 후

### 수정 전

```csharp
namespace Day04_Async;

class Program
{
    static async Task Main(string[] args)
    {
        AsyncBird bird = new AsyncBird();

        Task bird1 = bird.Bird1Async();
        Task bird2 = bird.Bird2Async();
        Task bird3 = bird.Bird3Async();

        await Task.WhenAll(bird1, bird2, bird3);

        Console.WriteLine("모든 새소리 끝");
    }
}

public class AsyncBird
{
    public async Task Bird1Async()
    {
        await Task.Delay(1000);
        Console.WriteLine("꾸우");

    }

    public async Task Bird2Async()
    {
        await Task.Delay(2000);
        Console.WriteLine("까악");
    }

    public async Task Bird3Async()
    {
        await Task.Delay(3000);
        Console.WriteLine("짹짹");
    }
}
```

### 수정 후

```csharp
namespace Day04_Async;

class Program
{
    static async Task Main(string[] args)
    {
        AsyncBird bird = new AsyncBird();

        Task bird1 = bird.Bird1Async();
        Task bird2 = bird.Bird2Async();
        Task bird3 = bird.Bird3Async();

        await Task.WhenAll(bird1, bird2, bird3);

        Console.WriteLine("모든 새소리 끝");
    }
}

public class AsyncBird
{
    public async Task Bird1Async()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine("꾸우");
        }
    }

    public async Task Bird2Async()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(2000);
            Console.WriteLine("까악");
        }
    }

    public async Task Bird3Async()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(3000);
            Console.WriteLine("짹짹");
        }
    }
}
```

각 새가 같은 소리를 4번씩 출력해야 했기 때문에 
같은 코드를 반복해서 작성하는 것보다 `for`문을 사용하는 것이 
더 간결하고 수정하기 쉽다고 판단

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용하지 않음
- 판단한 이유: 강의자료를 참고해 작성

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: 
꾸우
꾸우
까악
꾸우
짹짹
꾸우
까악
까악
짹짹
까악
짹짹
짹짹
모든 새소리 끝

- 추가로 확인한 내용: 세 비동기 작업이 동시에 실행되며,
모든 작업이 끝나고 "모든 새소리 끝"을 출력 후에
프로그램 종료가 되는 것을 확인

## 5. 아직 궁금한 점

`Task.WhenAll()`과 각각의 `Task`에 직접 `await`를 사용하는 방식의 차이를 
더 알아보고 싶다.

## 6. 다음에 적용할 것

서로 독립적으로 실행할 수 있는 작업은 각각 비동기 함수로 
분리하고, 여러 작업의 종료를 기다릴 때 `Task.WhenAll()`을 
사용해볼 것이다.
