namespace Day03_File;

class Program
{
    static void Main(string[] args)
    {
        string sourcePath = args[0];
        string destPath = args[1];

        IFileCopier fileCopier = new DefaultFileCopier();
        fileCopier.CopyFile(sourcePath, destPath);
    }
}