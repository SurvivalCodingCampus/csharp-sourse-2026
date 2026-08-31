using System.Diagnostics;

namespace Day03_Exception_File;

public interface IfileCopier
{
    void CopyFIle(string sourceFilePath, string destinationFilePath)
    {
        
        if (File.Exists(sourceFilePath))
        {
            // 파일 생성
            File.Copy(sourceFilePath, destinationFilePath, true);
            //AssetDatabase.Refresh(); 유니티(Unity) 에디터 외부에서 폴더나 파일이 추가, 수정, 삭제 시 유니티 데이터베이스를 갱신하는 함수
            Debug.Log("File copied successfully!");
        }
        else
        {
            Debug.LogError("File not found at path: " + sourceFilePath);
        }
        
        //참고 https://moondongjun.tistory.com/109
        
        //https://kdsoft-zeros.tistory.com/49

        
        
    }
    
        
        
        
}