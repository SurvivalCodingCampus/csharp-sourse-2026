using System.Diagnostics.Contracts;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Day03_OOP_FileAndException;
//2. 파일조작
/*
연습문제 요구사항
파일을 복사하는 DefaultFileCopier 클래스를 작성하시오
원본 파일 경로와 복사할 파일 경로는 프로그램 실행시 파라미터로 전달되는 것으로 하고, 예외 처리는 자유롭게 할 것
*/

// 상속 인터페이스
public interface IFileCopier
{
    void CopyFile(string sourceFilePath, string destinationFilePath);
}

// 복사할 내용 인터페이스 정의
public class DefaultFileCopier : IFileCopier
{
    public static void Main(string[] args)
    {
        //1. 파일 쓰기(파일 만들기) 📌원본 저장 개념
        string s = "Hello World";
        // File.WriteAllText("파일 '이름'이 작성되어야 할 곳", text); //text||s로 쓸거다라는 의미
        File.WriteAllText("Hello World", s);
        

        //2. 파일 복사(인터페이스함수 만들어진거에 정의해서 copyFile로 수정하겠다는 로직 )
        void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath, true); // 경로가 true이면 복사를 하겠다는 뜻
        }
        // 2-1. 파일 복제본을 만든것
        CopyFile("text.txt", "teeeeet.txt");

        
        //3. 파일 읽기
        string ss = File.ReadAllText("teeeeet.txt"); //📌 똑같은 변수명에 다른 파일(ss)을 넣을 수 없으므로 새로 정의하여 넣어야 한다
        
        
        
//3. 여러가지 데이터 형식
/*
총무부 리더 ‘홍길동(41세)’의 인스턴스를 생성하고 직렬화하여 company.json 파일에 Json String 형태로 저장하는 프로그램을 작성하시오.
직렬화를 위해 위의 2개 클래스를 일부 수정이 필요하면 하시오.
// 📌 붕어빵들 찍어내기
*/
        Employee e = new Employee("홍길동", 41);
        Department d = new Department("총무부", e);
        
        // 이거 안쓰면 한글 깨짐
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        //직렬(Serialization)
        string jsonString = JsonSerializer.Serialize(d, options);
        File.WriteAllText("company.json", jsonString);
    }
    public void CopyFile(string sourceFilePath, string destinationFilePath)
    {
        throw new NotImplementedException();
    }
}
