namespace Day01_OOP_Review;

class Program
{
    static void Main(string[] args)
    {
        User user = new User();
        Console.WriteLine(user.Name);
        user.Name = "Ja";
    }
}