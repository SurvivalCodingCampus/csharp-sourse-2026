namespace Day03_Exception_File;

public class DefaultFileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        string text = File.ReadAllText(sourceFilePath);
        File.AppendAllText(destinationFilePath, text);
    }
}

public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}