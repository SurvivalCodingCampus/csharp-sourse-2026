namespace Day04_Async;

class Program
{
    static async Task Main(string[] args)
    {
        const int repeatCount = 4;
        BirdSound birdSound = new BirdSound();
        
        Task firstBirdTask = birdSound.FirstBird(repeatCount);
        Task secondBirdTask = birdSound.SecondBird(repeatCount);
        Task thirdBirdTask = birdSound.ThirdBird(repeatCount);
        
        await Task.WhenAll(firstBirdTask, secondBirdTask, thirdBirdTask);
        
        Console.WriteLine("---------------");
        
    }
}

public class BirdSound
{
    private const int FirstBirdDelay = 1000;
    private const int SecondBirdDelay = 2000;
    private const int ThirdBirdDelay = 3000;

    
    public async Task FirstBird(int repeatCount) 
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Console.WriteLine("꾸우");
            await Task.Delay(FirstBirdDelay);
        }
    }
    
    public async Task SecondBird(int repeatCount) 
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Console.WriteLine("까악");
            await Task.Delay(SecondBirdDelay);
        }
    }
    
    public async Task ThirdBird(int repeatCount) 
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Console.WriteLine("짹짹");
            await Task.Delay(ThirdBirdDelay);
        }
    }
}


