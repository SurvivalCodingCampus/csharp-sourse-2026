using Day05_DataSource.DataSources;
using Day05_DataSource.Interfaces;

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
    }
}