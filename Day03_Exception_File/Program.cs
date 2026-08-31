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
        
        //직렬화 연습문제
        // 1. 홍길동(41세) Employee 인스턴스 생성
        Employee leader = new Employee("홍길동", 41);

        // 2. 총무부 Department 인스턴스 생성
        Department department = new Department("총무부", leader);

        // 3.-1 Department 객체를 JSON 문자열로 직렬화
        string jsonString = JsonSerializer.Serialize(
            department,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );
        
        //3.-2 동일 내용
        // JsonSerializerOptions options = new JsonSerializerOptions();
        //
        // options.WriteIndented = true;
        //
        // string jsonString = JsonSerializer.Serialize(
        //     department,
        //     options
        // );

        // 4. JSON 문자열을 company.json 파일에 저장
        File.WriteAllText("company.json", jsonString);

        // 확인
        Console.WriteLine(jsonString);


       
    }
}