using System;

public class Cleric
{
    public string Name {get ; private set;}
    public Random random = new Random();
    
    const int MaxHp = 50;
    const int MaxMp = 10;
    public int Hp = MaxHp;
    public int Mp = MaxMp;
    
    public Cleric(String name, int hp, int mp)
    {
        // 이름이 null이거나 띄어쓰기/빈 문자열인 경우 생성 자체를 막음
        if (name is null)
        {
            throw new ArgumentNullException(
                nameof(name)
            );
        }

        Name = name;
        Hp = hp;
        Mp = mp;
    }
    
    public int Pray(int seconds)
    {
        if (Mp < 1)
        {
            return 0;
        }

        int randomBonusMp = random.Next(3); // 3: 0~2를 의미 :  Pray(int seconds) 는 seconds + 0~2 만큼 MP를 회복
        Mp = Math.Min(Mp + randomBonusMp, MaxMp);

        return randomBonusMp;
    }

    public void SelfAid()
    {
        if (Mp < 5)
        {
            return;
            
        }
        Mp -= 5;
        Hp -= MaxHp;
    }
}