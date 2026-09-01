using Day03_Exception_File;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace Day03_Exception_File_Test;

public class Tests
{

    // [Test]
    // public void No_txt_에_들어_있는_내용을_Yes_txt_에_붙여넣기()
    // {
    //     File.WriteAllText("Yes.txt", "Yes");
    //     File.WriteAllText("No.txt", "No");
    //
    //     DefaultFileCopier copier = new DefaultFileCopier();
    //
    //     copier.CopyFile("No.txt", "Yes.txt");
    //
    //     string result = File.ReadAllText("Yes.txt");
    //
    //     Assert.That(result, Is.EqualTo("YesNo"));
    //     Console.WriteLine(result);
    // }
    
    [Test]
    public void 부서_이름_나이_값_확인()
    {
        Department department = new Department("총무부", new Employee("홍길동", 41));

        Assert.That(department.Name, Is.EqualTo("총무부"));
        Assert.That(department.leader.Name, Is.EqualTo("홍길동"));
        Assert.That(department.leader.Age, Is.EqualTo(41));
    }

    [Test]
    public void json_직렬화_값_확인()
    {

        Department department = new Department("총무부", new Employee("홍길동", 41));

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        
        string json = JsonSerializer.Serialize(department, options);

        Assert.That(json, Does.Contain("총무부"));
        Assert.That(json, Does.Contain("홍길동"));
        Assert.That(json, Does.Contain("41"));

        Console.WriteLine(json);
    }
    
    [Test]
    public void json_역직렬화_값_확인()
    {
        Department department = new Department("총무부", new Employee("홍길동", 41));

        string json = JsonSerializer.Serialize(department);

        Department? result = JsonSerializer.Deserialize<Department>(json);
        
        Assert.That(result.Name, Is.EqualTo("총무부"));
        Assert.That(result.leader.Name, Is.EqualTo("홍길동"));
        Assert.That(result.leader.Age, Is.EqualTo(41));

        Console.WriteLine(result.Name);
        Console.WriteLine(result.leader.Name);
        Console.WriteLine(result.leader.Age);
        
    }
}