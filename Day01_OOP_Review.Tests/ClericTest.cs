using System;
using Day01_OOP_Review;
using NUnit.Framework;

namespace Day01_OOP_Review.Tests;

[TestFixture]
[TestOf(typeof(Cleric))]
public class ClericTest
{
    [Test]
    public void Name_Test()
    {
        Assert.Throws<ArgumentException>(() => new Cleric("", 50, 10));
        Assert.Throws<ArgumentException>(() => new Cleric(null!, 50, 10));
    }
    
    [Test]
    public void SelfAid_Test()
    {
        var cleric = new Cleric("Priest", 10, 10);

        cleric.SelfAid();

        Assert.That(cleric.Hp, Is.EqualTo(Cleric.MaxHp));
        Assert.That(cleric.Mp, Is.EqualTo(5));
    }
    
    [Test]
    public void Pray_Test()
    {
        var cleric = new Cleric("Priest", 50, 0);
        int praySeconds = 3;

        int recoveredMp = cleric.Pray(praySeconds);

        Assert.That(recoveredMp, Is.InRange(praySeconds, praySeconds + 2));
        Assert.That(cleric.Mp, Is.EqualTo(recoveredMp));
        Assert.That(cleric.Mp, Is.LessThanOrEqualTo(Cleric.MaxMp));
    }
}