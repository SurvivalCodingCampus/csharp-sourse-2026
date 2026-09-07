namespace Day03_Exception_File;

public class C09_2_ex
{
    public class DefaultFileCopier
    {
        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                Console.WriteLine("파일복사 성공");
            }
            catch (Exception e)
            {
                Console.WriteLine($"파일 복사 실패: {e.Message}");
            }
        }
    }

}