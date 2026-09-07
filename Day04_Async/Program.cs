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