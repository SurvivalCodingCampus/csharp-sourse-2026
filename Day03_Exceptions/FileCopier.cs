namespace Day03_Exceptions;
using System;
using System.IO;

public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}

public class DefaultFileCopier : IFileCopier
{
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        try
        {
            File.Copy(sourceFilePath, destinationFilePath);
            Console.WriteLine("파일 복사 완료");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("원본 파일을 찾을 수 없습니다.");
        }
        catch (IOException)
        {
            Console.WriteLine("이미 존재하는 파일이거나 입출력 오류가 발생했습니다.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("파일에 접근할 권한이 없습니다.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("사용법: program.exe [원본파일경로] [복사할파일경로]");
            return;
        }

        string sourceFilePath = args[0];
        string destinationFilePath = args[1];

        IFileCopier fileCopier = new DefaultFileCopier();
        fileCopier.CopyFile(sourceFilePath, destinationFilePath);
    }
}