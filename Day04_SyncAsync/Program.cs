namespace Day04_SyncAsync;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("-----새들의 울음소리----");

        Bird bird1 = new Bird("꾸우", 1000);
        Bird bird2 = new Bird("까악", 2000);
        Bird bird3 = new Bird("짹짹", 3000);

        Task task1 = bird1.SingAsync();
        Task task2 = bird2.SingAsync();
        Task task3 = bird3.SingAsync();

        await Task.WhenAll(task1, task2, task3);
        
        Console.WriteLine("\n---울음소리 끝남 프로그램 종료---");
    }
}