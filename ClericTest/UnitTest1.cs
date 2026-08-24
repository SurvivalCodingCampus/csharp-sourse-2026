namespace ClericTest;
using Day01_OOP_Review;

public class UnitTest1
{
    [Fact]
    public void SelfAid_테스트()
    {
        // Given
        Cleric cleric = new Cleric("김용운");
        cleric.Hp = 10;

        // When
        cleric.SelfAid();

        // Then
        Assert.Equal(Cleric.MaxHp, cleric.Hp);
        Assert.Equal(5, cleric.Mp);
    }
    [Fact]
    public void Pray_최대마나초과_테스트()
    {
        // Given
        Cleric cleric = new Cleric("김용운");
        cleric.Mp = 8;

        // When (10초 기도)
        int recovered = cleric.Pray(10);

        // Then (8 -> 10이 되었으므로 실제 회복량은 2여야 함)
        Assert.Equal(2, recovered);
        Assert.Equal(Cleric.MaxMp, cleric.Mp);
    }
}