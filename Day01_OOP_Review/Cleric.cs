using System;

public class Cleric
{
    string Name;
    int Hp;
    int MaxHp = 50;
    
    int Mp;   
    int MaxMp = 10;

    private Random random = new Random();


    public Cleric(String name)
    {
        if (name == null)
        {
            throw new ArgumentNullException("name");
        }
        Name = name;
    }
    
    int SelfAid()
    {
        if (Mp >= 5)
        {
            Mp -= 5;
            Hp = MaxHp;
        }
        return Hp;
        
    }

  

    int Pray(int seconds)
    {
        int originalMp = Mp;
        if(Mp < MaxMp)
        {
            Mp += seconds;
            int randomBonus = random.Next(0, 3);
            Mp += randomBonus;
        }

        int finalMp = Mp-originalMp; // 원래는 그냥  Mp만 작성하면 될 줄알았지만 실제  Mp반환을 위해 변수 추가 필요 
        return finalMp;
    }
}