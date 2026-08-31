using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Day03_Serialization;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day03_File.Tests;

[TestClass]
[TestSubject(typeof(Day03_Serialization.Program))]
public class SerializationTest
{

    private const string TestFilePath = "company.json";

    // 테스트가 끝난 후 생성된 임시 파일 정리
    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }
    
    [TestMethod]
    public void SerializationTest_()
    {
        var leader = new Emplovee("홍길동", 41);
        var department = new DeparTment("총무부", leader);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        string jsonString = JsonSerializer.Serialize(department, options);
        File.WriteAllText(TestFilePath, jsonString);
        
        string readJson = File.ReadAllText(TestFilePath);
        DeparTment deserializedDept = JsonSerializer.Deserialize<DeparTment>(readJson, options);
        
        Assert.IsTrue(File.Exists(TestFilePath),"JSON 파일이 정상적으로 생성되어야함");
        Assert.IsNotNull(deserializedDept,"역직렬화된 것은 null이 아니여야한다");

        Assert.AreEqual("총무부", deserializedDept.Name);
        
        Assert.IsNotNull(deserializedDept.leader);
        Assert.AreEqual("홍길동", deserializedDept.leader.Name);
        Assert.AreEqual(41, deserializedDept.leader.Age);

    }
}