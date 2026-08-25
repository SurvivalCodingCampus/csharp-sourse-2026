namespace Day01_OOP_Review;

public class Cleric
{
    public string Name { get; private set; }
    public int HP { get; private set; }
    public int MP { get; private set; }
    public const int MaxHP = 50;
    public const int MaxMP = 10;

    public Cleric(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("이름이 없는 성직자는 생성할 수 없습니다.");
        }

        Name = name;
        HP = MaxHP;
        MP = MaxMP;
    }

    /// <summary>
    /// MP 5를 소비하고 HP를 최대로 회복합니다.
    /// </summary>
    public void SelfAid()
    {
        if (MP >= 5)
        {
            MP -= 5;
            HP = MaxHP;
        }
    }

    /// <summary>
    /// 지정된 초(seconds)만큼 기도하여 MP를 회복합니다. (0~2 추가 회복)
    /// </summary>
    /// <param name="seconds">기도 시간</param>
    /// <returns>실제로 회복된 MP 양</returns>
    public int Pray(int seconds)
    {
        Random rand = new Random();
        // 0~2 사이의 랜덤 값 생성
        int bonus = rand.Next(0, 3); 
        int potentialRecovery = seconds + bonus;
            
        int oldMP = MP;
        // MP가 MaxMP를 넘지 않도록 계산
        MP = Math.Min(MaxMP, MP + potentialRecovery);
            
        return MP - oldMP; // 실제로 회복된 양 반환
    }
}