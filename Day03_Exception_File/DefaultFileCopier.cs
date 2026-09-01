namespace Day03_Exception_File;

public class DefaultFileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        try
        {
            string content = File.ReadAllText(sourceFilePath);
            File.WriteAllText(destinationFilePath, content);
        }
        catch (Exception e)
        {
            Console.WriteLine("복사실패");
        }
    }
}