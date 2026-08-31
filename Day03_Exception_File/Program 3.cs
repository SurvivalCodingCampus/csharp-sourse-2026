using System.Text.Json;

namespace Day03_Exception_File;

public class Program_3
{
    public static void Main3()
    {
        try
        {
            Employee leader = new Employee("홍길동", 41);
            Department department = new Department("총무부", leader);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true, 
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 한글 깨짐 방지
            };
            string jsonString = JsonSerializer.Serialize(department, options);

            File.WriteAllText("company.json", jsonString);
            string projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "company.json");
            File.WriteAllText(projectPath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생 : {ex.Message}");
        }
    }
}