namespace Day04_Async;

public class Bird
{
    public static async Task BirdSound1()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine("꾸우");
        }
    }
    
    public static async Task BirdSound2()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(2000);
            Console.WriteLine("까악");
        }
    }
    
    public static async Task BirdSound3()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(3000);
            Console.WriteLine("짹짹");
        }
    }
    
    static async Task Main(string[] args)
    {
        Console.WriteLine("프로그램 실행");

        await Task.WhenAll(BirdSound1(), BirdSound2(), BirdSound3());
        
        Console.WriteLine("프로그램 종료");
    }
}