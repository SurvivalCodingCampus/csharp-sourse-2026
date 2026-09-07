using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IUserDataSource
{
    public Task<List<Person>> GetPeopleAsync();
    public Task SavePeopleAsync(List<Person> people);
    
    
    
} 