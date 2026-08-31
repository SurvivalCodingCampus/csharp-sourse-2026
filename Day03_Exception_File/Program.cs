using System.Text.Json;

namespace Day03_Exception_File;

class Program
{
    static void Main(string[] args)
    {
        //예외 연습문제 1
        var numString = "10.5";
        int num = int.Parse(numString);
        //Console.WriteLine(num);

        try
        {
            Console.WriteLine(num);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        //예외 연습문제 2
        try
        {
            Console.WriteLine(num);
        }
        catch (Exception e)
        {
            num = 0;
            Console.WriteLine(num);
        }
        
       
       
    }
}