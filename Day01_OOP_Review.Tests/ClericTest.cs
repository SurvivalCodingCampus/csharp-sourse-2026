using System.Xml.Serialization;
using NUnit.Framework;
using System;
namespace Day01_OOP_Review.Tests;

[TestFixture]
[TestOf(typeof(Cleric))]
public class ClericTest
{
    [Test]
    public void Cleric_이름이없을때_예외발생()
    {
        // 1. 이름이 null일 때 오류가 나는지 검증
        Assert.Throws<ArgumentNullException>(() => new Cleric(null, 60, 11));

        // 2. 이름이 빈 문자열("")일 때 오류가 나는지 검증
        //Assert.Throws<ArgumentException>(() => new Cleric("", 60, 11));
    }

    [Test]
    public void ClericTest_SelfAid하면_Mp5감소_Hp꽉참()
    {
        Cleric c = new Cleric("cc", 60, 11);
        Assert.AreEqual(60, c.Hp);
        Assert.AreEqual(11, c.Mp);

        c.Hp -= 5;
        //Assert.AreEqual(55, c.Hp);
        
        c.SelfAid(); //hp가 감소했을때 SelfAid확인 차 추가한 코드 및 이걸 반영하여 결과를 아래 코드로 확인가능 
        
        Assert.AreEqual(5, c.Hp);
        Assert.AreEqual(6, c.Mp);
    }
}