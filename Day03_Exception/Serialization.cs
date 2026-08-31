using System;
using System.IO;
using System.Text.Json;

namespace Day03_Exception;

class MainProgram
{
    static void OldMain(string[] args)
    {
       
        Employee leader = new Employee("홍길동", 41);
        Department department = new Department("총무부", leader);

        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };
        
        string jsonString = JsonSerializer.Serialize(department, options);
        
        File.WriteAllText("company.json", jsonString);

        Console.WriteLine("company.json 파일 저장 완료!");
        Console.WriteLine(jsonString);
    }
}