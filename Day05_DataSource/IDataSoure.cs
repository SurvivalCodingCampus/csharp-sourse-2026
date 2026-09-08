namespace Day05_DataSource;

public interface IDataSoure
{
    Task<List<Person>> GetPeopleAsync();
    Task SavePeopleAsync(List<Person> people);
}