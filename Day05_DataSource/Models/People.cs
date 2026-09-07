namespace Day05_DataSource.Models;

public class People
{
    public Person Person { get; }

    public string Name => Person.Name;
    public int Age => Person.Age;

    public People(Person person)
    {
        Person = person;
    }
    
    
    
    protected bool Equals(People other)
    {
        return Person == other.Person;
    }

    public override string ToString()
    {
        return Person.ToString();
    }
}