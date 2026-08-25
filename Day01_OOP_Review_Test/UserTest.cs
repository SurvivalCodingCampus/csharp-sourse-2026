using Day01_OOP_Review;

namespace Day01_OOP_Review_Test;

public class Tests
{

    [Test]
    public void Cleric_생성()
    {
        Cleric cleric = new Cleric("성직자");

        Console.WriteLine($"이름: {cleric.Name}");
        Console.WriteLine($"HP: {cleric.Hp}");
        Console.WriteLine($"MP: {cleric.Mp}");
        
        Assert.That(cleric.Name, Is.EqualTo("성직자"));
        Assert.That(cleric.Hp, Is.EqualTo(50));
        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Cleric_이름_체크()
    {
        ArgumentException? ex = Assert.Throws<ArgumentException>(() =>
        {
            new Cleric("");
        });
        
        Console.WriteLine(ex.Message);

        Assert.That(ex.Message, Is.EqualTo("이름은 필수입니다."));
    }

    [Test]
    public void SelfAid_사용시_Mp_5_감소_모든_Hp_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Hp -= 25;
        Console.WriteLine($"SelfAid 사용 전 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 사용 전 Mp: {cleric.Mp}");
        
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 사용 후 Mp: {cleric.Mp}");
        Assert.That(cleric.Mp, Is.EqualTo(5));
        Assert.That(cleric.Hp, Is.EqualTo(50));
    }

    [Test]
    public void Mp_부족_회복_불가()
    {
        Cleric cleric = new Cleric("성직자");

        Console.WriteLine($"SelfAid 사용 전 Mp: {cleric.Mp}");
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 첫번째 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 첫번째 사용 후 Mp: {cleric.Mp}");
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 두번째 사용 후 Mp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 두번째 사용 후 Hp: {cleric.Mp}");
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 세번째 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 세번째 사용 후 Mp: {cleric.Mp}");

        Assert.That(cleric.Mp, Is.EqualTo(0));
    }

    [Test]
    public void Pray_3초시_3에서_5_만큼_MP_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Mp -= 10;

        Console.WriteLine($"Mp 0 확인: {cleric.Mp}");
        int result = cleric.Pray(3);

        Console.WriteLine($"Pray 사용 후 Mp: {cleric.Mp}");
        Assert.That(result, Is.InRange(3, 5));
        Assert.That(cleric.Mp, Is.InRange(3, 5));
    }

    [Test]
    public void Mp가_최대Mp_넘는지_체크()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.SelfAid();
        Console.WriteLine($"Mp: {cleric.Mp}");

        int seconds = 100;
        int result = cleric.Pray(seconds);

        Console.WriteLine($"Pray: {seconds}초");
        Console.WriteLine($"Mp 회복량: {result}");
        Console.WriteLine($"Mp: {cleric.Mp}");

        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Pray_회복된_MP를_반환()
    {
        Cleric cleric = new Cleric("성직자");

        
        cleric.SelfAid();
        Console.WriteLine($"Pray 사용 전 Mp: {cleric.Mp}");
        
        int result = cleric.Pray(2);
        Console.WriteLine($"Pray 사용 후 Mp: {cleric.Mp}");
        
        Assert.That(result, Is.InRange(2, 4));
        Assert.That(cleric.Mp, Is.InRange(7, 9));
    }
}