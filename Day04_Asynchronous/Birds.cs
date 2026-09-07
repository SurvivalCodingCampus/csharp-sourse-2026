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