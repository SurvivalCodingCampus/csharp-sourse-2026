/*
 한 새는 1초마다, 다른 새는 2초마다, 마지막 새는 3초마다 소리를 냅니다.

    요구사항
    각 새의 소리 타이밍을 재현하되, 각 새마다 하나의 비동기 함수를 사용하세요.
    각 비동기 함수는 4번만 출력한 후 완료되어야 합니다.
    첫 번째 새는 "꾸우" 소리를 냅니다.
    두 번째 새는 "까악" 소리를 냅니다.
    마지막 새는 "짹짹" 소리를 냅니다.
    모든 새소리가 끝나면 프로그램이 종료되어야 합니다.
*/
namespace Day04_OOP_SyncNAsynchronous;

public class BirdPerSound
{
    
    public static async Task FirstBirdSound()
    {
        for (int i = 1; i <= 4; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine("꾸우");
        }
    }
    
    public static async Task SecondBirdSound()
    {
        for (int i = 1; i <= 4; i++)
        {
            await Task.Delay(2000);
            Console.WriteLine("까악");
        }
    }
    
    public static async Task ThirdBirdSound()
    {
        for (int i = 1; i <= 4; i++)
        {
            await Task.Delay(3000);
            Console.WriteLine("짹짹");
        }
    }
    
    
    //프로그램 종료됨을 알리는 코드
    static async Task Main()
    {
        await Task.WhenAll(
            FirstBirdSound(),
            SecondBirdSound(),
            ThirdBirdSound()
        );
        
        Console.WriteLine("새들의 소리 종료");
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   
}