/*namespace Day03_OOP_FileAndException;

public class ExceptionPractice1N2
{
    public static void Main(string[] args)
    {
        try
        {
            var numString = "10.5";
            int num = int.Parse(numString);  // numstring을 넣어야지 오류가 안뜨는데 모르겠다.
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw; // string 10.5가 맞지 않다는 예외처리가 발생 > 에러를 일단 미룸...
            
        }
    }
}
static void SomeError()
{
    throw new ArgumentException("에러");
}
*/
//---------------------------------------
/*
public class ExceptionPractice1N2
{
    public static void Main(string[] args)
    {
        var numString = "10.5";
        int num = 0;

        try
        {
            num = int.Parse(numString); //이 부분은 try 밖에 넣으면 이미 error가 터질 수밖에 없는 구조:: int타입에 문자열을 넣으려는 것이므로 try안에 넣어서 예외가 되도록 해야한다
        }
        catch (FormatException)
        {
            num = 0;
            Console.WriteLine(num);
        }
        
    }
}
*/