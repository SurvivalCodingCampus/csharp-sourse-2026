namespace Day03_Exception;

class Program
{
    static void Main(string[] args)
    {
        var numString = "10.5";
        int num;
        try
        {
            num = int.Parse(numString);
        }
        catch (Exception e)
        {
            num = 0;
        }
        
        Console.WriteLine(num);
        //  at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
        
    }
}