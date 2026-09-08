using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IPersonDataSources
{
    Task<Person> GetPerson(string name);
    Task SavePerson(Person person);
}