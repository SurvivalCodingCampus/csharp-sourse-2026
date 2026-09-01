namespace Day04_SyncAsync;

public class Bird
{
    // readonly : 초기화하면 읽기 전용으로 된다.
    private readonly string _sound; // 울음소리
    private readonly int _intervalMs; //울음 주기
    private readonly int _maxCount; // 최대 울음 횟수
    public Bird(string sound, int intervalMs, int maxCount = 4)
    {
        _sound = sound;
        _intervalMs = intervalMs;
        _maxCount = maxCount;
    }

    public async Task SingAsync()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            await Task.Delay(_intervalMs);
            Console.WriteLine($"{_sound} ({i + 1}/{_maxCount})");
        }
    }
}