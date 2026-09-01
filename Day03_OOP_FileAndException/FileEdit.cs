namespace Day03_OOP_FileAndException;

 //상속 인터페이스
    public interface IFileCopier
    {
        void CopyFile(string sourceFilePath, string destinationFilePath);
    }
/*

    public calss FileEdit : IFileCopier
    {
        static void Main(string[] args)
        {
            void CopyFile(string sourceFilePath, string destinationFilePath)
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
            }
        }
    }
 */   