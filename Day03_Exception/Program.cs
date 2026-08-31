namespace Day03_Exception;

class Program
{
    static void Main(string[] args)
    {
        var numString = "10.5";
        int num = int.Parse(numString);
        Console.WriteLine(num);
        //  at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
        
    }
}