using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IPersonDataSource
{
    Task<Person> GetPerson(string Name);
    Task SavePerson(Person person);
}