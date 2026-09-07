using System.Diagnostics;

namespace Day04_Async;

class Program
{
    // 한 새는 1초마다, 다른 새는 2초마다, 마지막 새는 3초마다 소리를 냅니다. 
    //
    //     요구사항 
    //     각 새의 소리 타이밍을 재현하되, 각 새마다 하나의 비동기 함수를 사용하세요.
    //     각 비동기 함수는 4번만 출력한 후 완료되어야 합니다. ???????
    //     첫 번째 새는 "꾸우" 소리를 냅니다.
    //     두 번째 새는 "까악" 소리를 냅니다.
    //     마지막 새는 "짹짹" 소리를 냅니다.
    //     모든 새소리가 끝나면 프로그램이 종료되어야 합니다.

    public static async Task<int> GetInt1()
    {
        await Task.Delay(1000);
        Console.WriteLine("꾸우");
        return 1;
    }

    public static async Task<int> GetInt2()
    {
        await Task.Delay(1000);
        Console.WriteLine("까악");
        return 2;
    }

    public static async Task<int> GetInt3()
    {
        await Task.Delay(1000);
        Console.WriteLine("짹짹");
        return 3;
    }
    
    
    
    public static async Task PrintInts()
    {
        List<int> results = new List<int>();
        results.Add(await GetInt1());
        results.Add(await GetInt2());
        results.Add(await GetInt3());
        
        

    }

    public static async Task Main()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await PrintInts();

        stopwatch.Stop();
        
        Console.WriteLine("프로그램 종료");
        
    }
    

}