namespace Day04_AsynchronousPrograming;

class Program
{
    static async Task Main(string[] args)
    {
        Birds birds = new Birds("꾸우", "까악", "짹짹");
        
        Task birdOneTask = birds.birdOneSound();
        Task birdTwoTask = birds.birdTwoSound();
        Task birdThreeTask = birds.birdThreeSound();

        await Task.WhenAll(
            birdOneTask,
            birdTwoTask,
            birdThreeTask
        );

        Console.WriteLine("모든 새소리가 끝났습니다.");
    }

    
}