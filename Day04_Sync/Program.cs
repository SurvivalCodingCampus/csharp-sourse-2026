using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static Stopwatch stopwatch = new Stopwatch();

    static async Task Main(string[] args)
    {
        stopwatch.Start();
        
        Task bird1 = tweet("Bird 1","꾸우", 1);
        Task bird2 = tweet("Bird 2","까악", 2);
        Task bird3 = tweet("Bird 3","짹짹", 3);

        await Task.WhenAll(bird1, bird2, bird3);

        stopwatch.Stop();
        Console.WriteLine($"All process end. Total time spend: {stopwatch.Elapsed.TotalSeconds:F1}s");
    }

    static async Task tweet(string birdName, string sound, double intervalSec)
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(intervalSec));
            Console.WriteLine($"[{stopwatch.Elapsed.TotalSeconds:F1}s] {sound} {birdName} ({i+1}번째)");
        }
    }
}