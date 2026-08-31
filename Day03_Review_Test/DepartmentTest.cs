using System;
using System.IO;
using System.Text.Json;
using Day03_예외파일조작데이터식;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace Day03_Review_Test;

[TestFixture]
[TestOf(typeof(Department))]
public class DepartmentTest
{

    [Test]
    public void METHOD()
    {
        // Arrange
        //9-2
        string Writing = "Hello World";
        File.WriteAllText("sourceFilePath.txt", Writing);
        
        //9-3
        Employee employee = new Employee("HongGilDong", 41);
        Department department = new Department("Secretary", employee); //총무 번역시 Secretary로 번역
        // Act
        //9-2
        DefaultFilecopier defaultFilecopier = new DefaultFilecopier(Writing, "destinationFilePath.txt");
        byte[] originalBytes = File.ReadAllBytes("sourceFilePath.txt");
        byte[] copiedBytes = File.ReadAllBytes("destinationFilePath.txt");
        
        //9-3
        string jsonString = JsonSerializer.Serialize(department);
        using JsonDocument json = JsonDocument.Parse(jsonString);

        JsonElement root = json.RootElement;
        JsonElement leader = root.GetProperty("Leader");
        
        Department? loadedDepartment = JsonSerializer.Deserialize<Department>(jsonString);

        Console.WriteLine("Act 성공시 출력");
        //9-2
        // Assert
        Assert.AreEqual(originalBytes, copiedBytes);
        
        //9-3
        // Assert 1 json 내부 값 검사
        Assert.AreEqual("Secretary", root.GetProperty("Name").GetString());
        Assert.AreEqual("HongGilDong", leader.GetProperty("Name").GetString());
        Assert.AreEqual(41, leader.GetProperty("Age").GetInt32());
        // Assert 2 생성자 자체 검사
        Assert.AreEqual("Secretary", department.Name);
        Assert.AreSame(employee, department.Leader);
        Assert.AreEqual("HongGilDong", department.Leader.Name);
        Assert.AreEqual(41, department.Leader.Age);
        Console.WriteLine("Assert 성공시 출력");
    }
}