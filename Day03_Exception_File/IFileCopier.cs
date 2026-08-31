namespace Day03_Exception_File;

public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}

public class FileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        try
        {
            string temp = File.ReadAllText(sourceFilePath);
            File.WriteAllText(destinationFilePath, temp);
            // File.Copy(sourceFilePath, "UsingFileCopy" + destinationFilePath, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        
    }
}
