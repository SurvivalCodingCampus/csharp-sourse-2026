using System.Text.Json;

namespace Day03_예외파일조작데이터식;

class Program
{
    static void Main(string[] args)
    {
        //연습 문제 1
        //다음과 같은 코드를 작성, 실행 후 runtime error 를 발생시키고, 어떤 Exception이 발생하는지 확인
        //var numstring = "10.5";
        //int num = int.Parse(numstring);
        //오류 로그 (10.5를 정수로 변환 할수 없음)
        //System.FormatException: The input string '10.5' was not in a correct format.
        // at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
        // at System.Int32.Parse(String s)
        // at Day03_예외파일조작데이터식.Program.Main(String[] args) in C:\Users\USER\RiderProjects\CS\Day03_Exception_File_Others\Program.cs:line 11
        //Console.Write(num);
        Console.WriteLine( "9-1 연습 문제 1번. 다음과 같은 코드를 작성, 실행 후 runtime error 를 발생시키고, 어떤 Exception이 발생하는지 확인");
        //연습 문제 2
        //연습 1 에서 작성한 코드를 수정하여, try-catch() 문을 사용하여 예외처리를 하시오. 예외처리에는 다음의 처리를 수행하시오.
        //예외가 발생하면 num을 0으로 처리
        var numString = "10.5";
        int num;
        
        try
        {
            num = int.Parse(numString);
        }
        catch(Exception)
        {
            num = 0;
        }
        Console.WriteLine("9-1 연습 문제 2번. 연습 1 에서 작성한 코드를 수정하여, try-catch() 문을 사용하여 예외처리를 하시오. 예외처리에는 다음의 처리를 수행하시오. 예외가 발생하면 num을 0으로 처리");
        Console.WriteLine(num);
        
        Console.WriteLine("9-2 파일을 복사하는 DefaultFileCopier 클래스를 작성하시오. 원본 파일 경로와 복사할 파일 경로는 프로그램 실행시 파라미터로 전달되는 것으로 하고, 예외 처리는 자유롭게 할 것.");
        string Writing = "Hello World";
        File.WriteAllText("SourceFilePath.txt", Writing);
        DefaultFilecopier defaultFilecopier = new DefaultFilecopier("SourceFilePath.txt", "destinationFilePath.txt");

        Console.WriteLine(Writing);

        
        Console.WriteLine("9-3 총무부 리더 ‘홍길동(41세)’의 인스턴스를 생성하고 직렬화하여 company.json 파일에 Json String 형태로 저장하는 프로그램을 작성하시오. 직렬화를 위해 위의 2개 클래스를 일부 수정이 필요하면 하시오.");
        Employee employee = new Employee("HongGilDong", 41);
        Department department = new Department("Secretary", employee); //Secretary(총무)
        string JsonString = JsonSerializer.Serialize(department);
        
        File.WriteAllText("department.json", JsonString);
        
        Department? loadedDepartment = JsonSerializer.Deserialize<Department>(JsonString);
        string text = File.ReadAllText("department.json");
        Console.WriteLine(text);
    }
}