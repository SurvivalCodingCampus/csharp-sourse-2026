using System.Text.Json;
using System.Text.Encodings.Web;



namespace Day03_Exception_File;

class Program
{
    static void Main(string[] args)
    {
        // var numString = "10.5";
        // int num;
        // try
        // {
        //     num = int.Parse(numString);
        // }
        // catch (FormatException e)
        // {
        //     Console.WriteLine(e.Message);
        //     Console.WriteLine("예외처리 발생");
        //     
        //     num = 0;
        // }
        // Console.WriteLine(num);

        // // 새로 쓰기
        // File.WriteAllText("text.txt", "Hello World!");
        // File.WriteAllText("text.txt", "Hello World!");
        //
        //
        // // 뒤에 붙이기(Append)
        // File.AppendAllText("text.txt", "붙이기");
        // File.AppendAllText("text.txt", "붙이기");
        // File.AppendAllText("text.txt", "붙이기");
        //
        // // 붙이고 내리기
        // File.AppendAllText("text.txt", "내리기\n");
        // File.AppendAllText("text.txt", "내리기\n");
        // File.AppendAllText("text.txt", "내리기\n");
        //
        // string text = File.ReadAllText("text.txt");
        //
        // string[] lines = File.ReadAllLines("text.txt");
        //
        // try
        // {
        //     File.ReadAllText("test.txt");
        // }
        // catch(FileNotFoundException e)
        // {
        //     Console.WriteLine("파일이 없습니다.");
        // }
        
        // Hero hero = new Hero("text", 100);
        
        // // 직렬화
        // string json = JsonSerializer.Serialize(hero);
        //
        // var option = new JsonSerializerOptions();
        // option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //
        // Hero? hero2 = JsonSerializer.Deserialize<Hero>(json);
        // if (hero2 is null)
        // {
        //     Console.WriteLine("null");
        // }
        // Console.WriteLine(hero);
        // Console.WriteLine(hero2);
        // Console.WriteLine(hero.Equals(hero2));
        
        // // 파일 복사 연습문제
        // DefaultFileCopier copier = new DefaultFileCopier();
        //
        // copier.CopyFile(args[0], args[1]);
        

        Department department = new Department("총무부", new Employee("홍길동", 41));

        // 이거 안쓰면 한글 깨짐
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        string json = JsonSerializer.Serialize(department, options);

        File.WriteAllText("company.json", json);
        
        
        string jsonText = File.ReadAllText("company.json");

        Department? result = JsonSerializer.Deserialize<Department>(jsonText);

        if (result != null)
        {
            Console.WriteLine(result.Name);
            Console.WriteLine(result.leader.Name);
            Console.WriteLine(result.leader.Age);
        }
    }
}