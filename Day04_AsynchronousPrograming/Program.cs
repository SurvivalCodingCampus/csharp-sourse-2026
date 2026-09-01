namespace Day04_AsynchronousPrograming;

class Program
{
    static async Task Main(string[] args)
    {
        Birds birds = new Birds("꾸우", "까악", "짹짹");
        for (int i = 0; i < 4; i++)
        {
            birds.birdOneSound();
            birds.birdTwoSound();
            birds.birdThreeSound();
        }
        Console.WriteLine("프로그램 종료");
    }

    
}