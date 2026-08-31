using Day01_OOP_Review;
using NUnit.Framework;

namespace Day01_OOP_Review.Tests;

[TestFixture]
[TestOf(typeof(Cleric))]
public class ClericTest
{

    [Test]
    public void SelfAid_하면_Mp가_5감소하고_Hp가_꽉_차야_됨()
    {
        Cleric cleric = new Cleric("test", 45, 10);

        Assert.That(cleric.Hp, Is.EqualTo(45));
        Assert.That(cleric.Mp, Is.EqualTo(10));

        cleric.SelfAid();

        Assert.That(cleric.Mp, Is.EqualTo(5));
        Assert.That(cleric.Hp, Is.EqualTo(50));
    }
}