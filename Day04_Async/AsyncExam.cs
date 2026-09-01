namespace Day04_Async;

public class AsyncExam
{
    // 데이터 읽어오겠다
    public void FetchData(Action<string> onSuccess)
    {
        Console.WriteLine("Fetching data1...");
        
        Task.Delay(1000).ContinueWith(_ => onSuccess("Success"));
        
        Console.WriteLine("Fetching data2...");
    }
}