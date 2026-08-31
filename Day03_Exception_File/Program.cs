using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Day03_Exception_File;

class Program
{
    static void Main(string[] args)
    {
        // 과제 1 예외처리
        var numString = "10.5";
        int num;
        try
        {
            num = int.Parse(numString);
            Console.WriteLine(num);
        }
        catch (Exception e)
        {
            num = 0;
        }
        
        // 과제 3 Json 직렬화
        Employee employee = new Employee("홍길동", 41);
        Department department = new Department("총무부", employee);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        string departmentString = JsonSerializer.Serialize(department, options);
        
        File.WriteAllText("company.json", departmentString);

        // 과제 2 파일 조작
        FileCopier fileCopier = new FileCopier();
        fileCopier.CopyFile("company.json", "CopyCompany.json");
    }
}