using System.Text.Json;

namespace Day03_Exception_File;

class Program
{
    static void Main(string[] args)
    {
        var option = new JsonSerializerOptions();
        option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        var hero = new Hero("Batman", 100);
        
        // 직렬화
        string json = JsonSerializer.Serialize(hero);
        
        Hero? hero2 = JsonSerializer.Deserialize<Hero>(json);
        if (hero2 is null)
        {
            Console.WriteLine("null");
        }
        Console.WriteLine(hero2);
        
        Console.WriteLine(hero.Equals(hero2));   // true
        
        // 새로 쓰기
        File.WriteAllText("text.txt", "Hello World!");
        File.WriteAllText("text.txt", "Hello World!");
        
        // 뒤에 붙이기
        File.AppendAllText("text.txt", "붙이기");
        File.AppendAllText("text.txt", "붙이기");
        File.AppendAllText("text.txt", "붙이기");
        
        // 붙이고 내리기
        File.AppendAllText("text.txt", "내리기\n");
        File.AppendAllText("text.txt", "내리기\n");
        File.AppendAllText("text.txt", "내리기\n");
        
        string text = File.ReadAllText("text.txt");
        
        string[] lines = File.ReadAllLines("text.txt");

        try
        {
            File.ReadAllText("testttttt.txt");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("파일이 없습니다");
        }
        
    }
}