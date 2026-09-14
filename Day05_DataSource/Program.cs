using Day05_DataSource.DataSources;
using Day05_DataSource.Interfaces;
using Day05_DataSource.Models;

namespace Day05_DataSource;

class Program
{
    static Task<List<string>> GetNames()
    {
        return Task.FromResult<List<string>>([]);
    }
    static async Task Main(string[] args)
    {
        IPersonDataSource dataSource = new FilePersonDataSource();

        Person person = await dataSource.GetPerson("오준석");
        Task<string> text = File.ReadAllTextAsync("person.json");
        
        
        // UI
        Console.WriteLine(person);

        Person newPerson = new Person(name : "홍길동", age : 10);
        await dataSource.SavePerson(newPerson);
    }
}