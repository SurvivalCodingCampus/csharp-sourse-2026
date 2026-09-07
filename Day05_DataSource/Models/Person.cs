namespace Day05_DataSource.Models;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Person()
    {
        Name = "";
        Age = 0;
    }


    protected bool Equals(Person other)
    {
        return Name == other.Name && Age == other.Age;
    }

    public override string ToString()
    {
        return  $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }
}