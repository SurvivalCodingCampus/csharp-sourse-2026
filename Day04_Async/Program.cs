using Day04_Async;

var asyncExam = new AsyncExam();
asyncExam.FetchData(result =>
{
    Console.WriteLine("콜백 : " + result);
});


Console.WriteLine("복사 시작");

await File.WriteAllTextAsync("text.txt", "Hello World");
    
Console.WriteLine("복사 끝");
    

// 3초 대기
await Task.Delay(3000);

Console.WriteLine("3초 끝");

Console.WriteLine("프로그램 끝");

// 메인 스레드 잡고 있는 놈
Console.ReadLine();
