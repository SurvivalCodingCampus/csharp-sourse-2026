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
        Cleric cleric = new Cleric("test", 50, 10);
        Assert.AreEqual(50, cleric.Hp);
        Assert.AreEqual(10, cleric.Mp);

        cleric.Hp -= 5;
        
        cleric.SelfAid();
        
        Assert.AreEqual(5, cleric.Mp);
        Assert.AreEqual(50, cleric.Hp);
    }
}