namespace Day04_DataSource;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public Person()
    {
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}