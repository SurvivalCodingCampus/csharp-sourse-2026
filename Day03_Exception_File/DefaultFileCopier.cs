using System.Text.Json;

namespace Day03_Exception_File;

public class DefaultFileCopier : IFileCopier
{
    public static void Main(string[] args)
    {
        // 파일 쓰기
        string text = "Hello, world!";
        File.WriteAllText("text.txt",text);
        File.AppendAllText("text.txt","\n내려쓰기\n");
        
        void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            File.Copy("text.txt","copytext", true);
        }
        
        // 파일 복사
        CopyFile("text.txt","copytext");
        
        
        // company
        Employee employee = new Employee("HongGilDong", 41);
        Department department = new Department("TeamLeader", employee);
        
        string jsonString = JsonSerializer.Serialize(department);
        File.WriteAllText("company.json", jsonString);

    }

    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        throw new NotImplementedException();
    }
}