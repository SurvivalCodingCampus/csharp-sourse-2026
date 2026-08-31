using System.Text.Encodings.Web;
using System.Text.Json;

namespace Day03_Exception_File;

class Program
{
    static void Main(string[] args)
    {
        var option = new JsonSerializerOptions();
        option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        option.WriteIndented = true;
        option.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        string sPath = "source.txt";
        string dPath = "destination.txt";
        Employee hong = new Employee("홍길동", 41);
        Department dept = new Department("총무부", hong);
        
        File.WriteAllText(sPath, "과제과제과제");
        var numString = "10.5";
        int num;
        try
        {
            
            num = int.Parse(numString);
            Console.WriteLine(num);
        }
        catch (FormatException e)
        {
            num = 0;
        }
        
        Console.WriteLine(num);
        
        IFileCopier copier = new DefaultFileCopier();
        copier.CopyFile(sPath, dPath);
        
        
        string jsonString = JsonSerializer.Serialize(dept, option);
        string filePath = "company.json";
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine(jsonString);
    }
}