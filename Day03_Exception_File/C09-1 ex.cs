namespace Day03_Exception_File;


public class C09_1_ex
{
        public static void Main1()
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
                        Console.WriteLine(e.Message);
                }

                Console.WriteLine($"num의 값 : {num}");
        }
}