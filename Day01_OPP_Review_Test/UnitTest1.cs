using Day01_OOP_Review;

namespace Day01_OPP_Review_Test;
using NUnit.Framework;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
        
        
    }

    [Test]
    public void Test1()
    {
        var cleric = new Cleric("Degtayov", 25, 10);
        cleric.selfAid();
        
        Assert.AreEqual(5, cleric.Mp);
        
        cleric.pray(3);
        
        Assert.AreEqual(10, cleric.Mp);

        
        Assert.Pass();
    }
}