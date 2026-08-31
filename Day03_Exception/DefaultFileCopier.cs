namespace Day03_Exception;

public class DefaultFileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        String content = File.ReadAllText(sourceFilePath);
        File.WriteAllText(destinationFilePath, content);
    }
}
   
