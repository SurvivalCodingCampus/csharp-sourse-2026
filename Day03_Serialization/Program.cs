using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Day03_Serialization;

class Program
{
    static void Main(string[] args)
    {
        Emplovee leader = new Emplovee("홍길동", 41);
        DeparTment department = new DeparTment("총무부", leader);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        string jsonString = JsonSerializer.Serialize(department, options);

        string filePath = "company.json";
        File.WriteAllText(filePath, jsonString);
        
        Console.WriteLine($"저장 완료 : {Path.GetFullPath(filePath)}");
        Console.WriteLine(jsonString);
    }
}