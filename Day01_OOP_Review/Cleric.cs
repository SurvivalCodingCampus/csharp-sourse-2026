namespace Day01_OOP_Review;

using System;

public class Cleric
{
    private const int MaxHp = 50;
    private const int MaxMp = 10;
    private const int SelfAidMpCost = 5;

    public string Name { get; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public Cleric(string name)
    {
        Name = name;
        Hp = MaxHp;
        Mp = MaxMp;
    }

    public void SelfAid()
    {
        if (Mp < SelfAidMpCost)
        {
            Console.WriteLine("MP가 부족합니다.");
            return;
        }

        Mp -= SelfAidMpCost;
        Hp = MaxHp;
    }

    public int Pray(int seconds)
    {
        if (seconds < 0)
        {
            Console.WriteLine("기도 시간은 0초 이상이어야 합니다.");
            return 0;
        }

        int recoveryAmount = seconds + Random.Shared.Next(0, 3);
        int previousMp = Mp;

        Mp = Math.Min(Mp + recoveryAmount, MaxMp);

        return Mp - previousMp;
    }
}