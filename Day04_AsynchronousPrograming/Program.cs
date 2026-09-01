namespace Day04_AsynchronousPrograming;

class Program
{
    static async Task Main(string[] args)
    {
        string bird1 = "꾸우";
        string bird2 = "까악";
        string bird3 = "짹짹";
        for (int i = 0; i < 4; i++)
        {
            Task.Delay(1000).Wait();
            Console.WriteLine(bird1);
        }
        for (int i = 0; i < 4; i++)
        {
            Task.Delay(2000).Wait();
            Console.WriteLine(bird2);
        }
        for (int i = 0; i < 4; i++)
        {
            Task.Delay(3000).Wait();
            Console.WriteLine(bird3);
        }
    }
}