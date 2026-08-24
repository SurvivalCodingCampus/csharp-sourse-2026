using Day01_OOP_Review;

namespace Day01_OOP_Review_Test;

public class Tests
{
    private const string TestName = "testName";
    private const int TestNumber = 5;
    private const int OverMaxHp = Cleric.MaxHp + 1;
    private const int OverMaxMp = Cleric.MaxMp + 1;
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ClericTest()
    {
        // Given
        Cleric testCleric = new(TestName, TestNumber, TestNumber);
        
        // When
        testCleric.SelfAid();
        
        int prayResult = testCleric.Pray(TestNumber);
        
        // Then
        Assert.That(testCleric.GetHp(), 
            Is.EqualTo(Cleric.MaxHp), 
            "SelfAid로 히복 여부 판별");
        Assert.That(prayResult, 
            Is.EqualTo(testCleric.GetMp()),
            "현재 MP가 0인 상황에서 Pray 시전시 회복량 반환 판별");
        Assert.Throws(typeof(ArgumentNullException), () =>
        {
            Cleric nameNull = new(null);
        });
    }

    [Test]
    public void OverMaxNumberTest()
    {
        // Given - When
        Cleric testCleric = new(TestName, OverMaxHp, OverMaxMp);
        
        // Then
        Assert.That(testCleric.GetHp(), Is.EqualTo(Cleric.MaxHp), "MaxHp 초과 값 여부 확인");
        Assert.That(testCleric.GetMp(), Is.EqualTo(Cleric.MaxMp), "MaxHp 초과 값 여부 확인");
    }

    [Test]
    public void NegativeNumberTest()
    {
        // Given - When
        Cleric testCleric = new(TestName, -1, -1);
        
        // Then 
        Assert.That(testCleric.GetHp(), Is.Zero);
        Assert.That(testCleric.GetMp(), Is.Zero);
    }
}