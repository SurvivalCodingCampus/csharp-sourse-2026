namespace Day01_OOP_Review_Test;

using NUnit.Framework;
using System;

public class ClericTests
{
    private Cleric cleric;

    [SetUp]
    public void Setup()
    {
        cleric = new Cleric("아서");
    }

    [Test]
    public void Cleric_생성자_기본값_설정_확인()
    {
        Assert.That(cleric.Name, Is.EqualTo("아서"));
        Assert.That(cleric.Hp, Is.EqualTo(Cleric.MaxHp));
        Assert.That(cleric.Mp, Is.EqualTo(Cleric.MaxMp));
    }

    [Test]
    public void Cleric_이름이_없거나_공백이면_예외_발생()
    {
        Assert.Throws<ArgumentException>(() => new Cleric(""));
        Assert.Throws<ArgumentException>(() => new Cleric("   "));
        Assert.Throws<ArgumentException>(() => new Cleric(null));
    }

    [Test]
    public void SelfAid_MP가_충분할때_HP가_최대로_회복되고_MP가_5_소모된다()
    {
        cleric.Hp = 10;
        cleric.Mp = 10;
        
        cleric.SelfAid();
        
        Assert.That(cleric.Hp, Is.EqualTo(Cleric.MaxHp)); // HP 50 회복
        Assert.That(cleric.Mp, Is.EqualTo(5));            // MP 10 - 5 = 5
    }

    [Test]
    public void SelfAid_MP가_부족하면_체력회복이_되지_않는다()
    {
        cleric.Hp = 10;
        cleric.Mp = 4;
        
        cleric.SelfAid();
        
        Assert.That(cleric.Hp, Is.EqualTo(10));
        Assert.That(cleric.Mp, Is.EqualTo(4));
    }

    [Test]
    public void Pray_기도시간이_0초_이하이면_0을_반환한다()
    {
        cleric.Mp = 0;

        int recovered = cleric.Pray(0);

        Assert.That(recovered, Is.EqualTo(0));
        Assert.That(cleric.Mp, Is.EqualTo(0));
    }

    [Test]
    public void Pray_MP가_최대치를_초과하지_않는다()
    {
        cleric.Mp = 9;
        
        int recovered = cleric.Pray(3);
        
        Assert.That(recovered, Is.EqualTo(1));
        Assert.That(cleric.Mp, Is.EqualTo(Cleric.MaxMp));
    }
}