namespace Day04_DataSource;
using System;
using System.Collections.Generic;
using System.Linq;


public class Program {
    static async Task Main(string[] args)
    {
        IDataSource dataSource = new JsonFileDataSource("people.json");
        //데이터를 불러와 필터링 하는 로직
        var people = await dataSource.GetPeopleAsync();
        var adults = people.Where(p => p.Age >= 19).ToList();
        Console.Write("성인만 출력");
        adults.ForEach(p => Console.WriteLine($"- {p.Name} ({p.Age}세"));
        //데이터를 추가하고 저장 하는 로직
        people.Add(new Person { Name = "김민지", Age = 15});

    await dataSource.SavePeopleAsync(people);
        Console.WriteLine("\n 김민지 추가후 저장 완료");
        //데이터를 저장하고 삭제 하는 로직
        var toDelete = people.FirstOrDefault(p => p.Name == "이민준");
        if (toDelete != null)
        {
            people.Remove(toDelete);
            await dataSource.SavePeopleAsync(people);
            Console.WriteLine("\n 이민준 삭제 후 저장 완료");
        }
    }
}