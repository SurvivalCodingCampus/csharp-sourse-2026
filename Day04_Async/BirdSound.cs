using System;
using System.Threading.Tasks;


    Console.WriteLine("==========================\n");

            
            Task bird1 = MakeBirdSoundAsync("꾸우", 1000, 4);
            Task bird2 = MakeBirdSoundAsync("까악", 2000, 4);
            Task bird3 = MakeBirdSoundAsync("짹짹", 3000, 4);

            
            await Task.WhenAll(bird1, bird2, bird3);

            Console.WriteLine("\n========= 프로그램 종료 =========");


   
        static async Task MakeBirdSoundAsync(string sound, int delayMs, int count)
        {
            for (int i = 0; i < count; i++)
            {
                
                await Task.Delay(delayMs);
                Console.WriteLine($"{sound} (시간: {DateTime.Now:HH:mm:ss})");
            }
        }
  