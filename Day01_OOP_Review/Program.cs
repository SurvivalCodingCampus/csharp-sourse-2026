namespace Day01_OOP_Review;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        Cleric cleric = new Cleric("Zero", 50, 10);
        
        cleric.SelfAid();
        Console.WriteLine(cleric.Mp);
        
        cleric.SelfAid();
        Console.WriteLine(cleric.Mp);
        
        cleric.SelfAid();
        Console.WriteLine(cleric.Mp);
        
        int Actualheal = cleric.Pray(3);
        Console.WriteLine(cleric.Mp);
        Console.WriteLine(Actualheal);
    }
}