namespace Day01_OOP_Review_Test;

[Test]
class Cleric
{
    void main(string[] args)
    {
        Cleric c = new Cleric("");
        Console.WriteLine(c);
//----------------------------------
        Cleric.Hp = 9;
        Console.Mp = 3;
        
        Console.WriteLine(Cleric.Hp);
        Console.WriteLine(Cleric.Mp);
//----------------------------------




    }
}