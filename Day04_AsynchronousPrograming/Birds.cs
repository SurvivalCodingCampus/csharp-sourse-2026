namespace Day04_AsynchronousPrograming;

public class Birds
{
    public string Bird1 { get; }
    public string Bird2 { get; }
    public string Bird3 { get; }

    public Birds(string bird1, string bird2,string bird3)
    {
        Bird1 = bird1;
        Bird2 = bird2;
        Bird3 = bird3;
    }
    
    public async Task birdOneSound()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine(Bird1);
        }

    }
    public async Task birdTwoSound()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(2000);
            Console.WriteLine(Bird2);
        }
    }
    public async Task birdThreeSound()
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(3000);
            Console.WriteLine(Bird3);
        }
    }
}

