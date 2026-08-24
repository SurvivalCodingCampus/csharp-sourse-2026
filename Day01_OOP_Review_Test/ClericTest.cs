using Day01_OOP_Review;

namespace Day01_OOP_Review_Test;

public class ClericTest
{
    private Cleric _cleric;
    private const int PraySec = 2;
    
    [SetUp]
    public void Setup()
    {
        _cleric = new Cleric("Hero");
    }

    [Test]
    public void NameTest()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Cleric cleric2 = new Cleric(null);
        });
        
        Assert.That(_cleric.Name, Is.EqualTo("Hero"));
    }

    [Test]
    public void SelfAidTest()
    {
        _cleric.SelfAid();
        Assert.That(_cleric.Mp, Is.EqualTo(5));
        
        _cleric.SelfAid();
        Assert.That(_cleric.Mp, Is.EqualTo(0));
        
        _cleric.SelfAid();
        Assert.That(_cleric.Mp, Is.EqualTo(0));
    }

    [Test]
    public void PrayTest()
    {
        _cleric.SelfAid();
        Assert.That(_cleric.Mp, Is.EqualTo(5));
        
        Assert.That(_cleric.Pray(PraySec), Is.InRange(2,4));

        int beforeMp = _cleric.Mp;
        int recoverMp = _cleric.Pray(PraySec);
        Assert.That(_cleric.Mp, Is.EqualTo(beforeMp + recoverMp));
    }
}