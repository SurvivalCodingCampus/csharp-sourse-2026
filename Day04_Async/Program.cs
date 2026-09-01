using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try{
            // 각 새의 비동기 함수를 동시에 호출하고, Task 객체로 받아둡니다.
            Task bird1 = SingBirdAsync("꾸우", 1000, 4);
            Task bird2 = SingBirdAsync("까악", 2000, 4);
            Task bird3 = SingBirdAsync("짹짹", 3000, 4);

            // 세 마리의 새가 모두 4번씩 울 때까지 메인 스레드가 기다립니다.
            await Task.WhenAll(bird1, bird2, bird3);

            Console.WriteLine("프로그램 종료");
        }catch(Exception ex){
            Console.WriteLine($"[에러 발생] 비동기 작업 중 문제가 생겼습니다: {ex.Message}");
        }

    }

    // 새가 지정된 간격으로 소리를 내는 비동기 함수
    static async Task SingBirdAsync(string sound, int intervalMs, int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            // 지정된 시간(ms)만큼 기다림 (앱을 멈추지 않음)
            await Task.Delay(intervalMs);
            Console.WriteLine(sound);
        }
    }
}