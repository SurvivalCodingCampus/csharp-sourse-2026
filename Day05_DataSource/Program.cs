using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day05_DataSource;

class Program
{
    static async Task Main(string[] args)
    {
        IDataSource dataSource = new JsonFileDataSource("people.json");

        var people = await dataSource.GetPeopleAsync();
        var adults = people.Where(p => p.Age >= 19).ToList();
        Console.WriteLine("성인만 출력:");
        adults.ForEach(p => Console.WriteLine($"- {p.Name} ({p.Age}세)"));

        if (!people.Any(p => p.Name == "김민지"))
        {
            people.Add(new Person { Name = "김민지", Age = 15 });
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n김민지 추가 후 저장 완료.");
        }

        var toDelete = people.FirstOrDefault(p => p.Name == "이민준");
        if (toDelete != null)
        {
            people.Remove(toDelete);
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n이민준 삭제 후 저장 완료.");
        }
    }   
}
