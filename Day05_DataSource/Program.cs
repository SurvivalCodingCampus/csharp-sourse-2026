using Day05_DataSource.DataSource;
using Day05_DataSource.Interface;
using Day05_DataSource.Model;

//import 'Day05_DataSource.Model.Person'

namespace Day05_DataSource;

class Program {
    static async Task Main(string[] args) {
        IDataSource DS = new JsontFileDataSource("people.json"); // 객체를 직접 생성 인스턴스
        
        //1. 데이터를 불러와 필터링
        var people = await DS.GetPeopleAsync();
        var adults = people.Where(p => p.Age >= 19).ToList();
        Console.WriteLine("성인만 출력: ");
        adults.ForEach(p => Console. WriteLine($"-{p.Name}, {p.Age}세"));
        
        //2. 데이터를 추가하고 저장
        people.Add(new Person { Name = "김민지", Age = 15 });
        await DS.SavePeopleAsync(people);
        Console.WriteLine("\n 김민지 추가 후 저장완료");
        
        //3. 데이터를 삭제
        var toDelete = people.FirstOrDefault(p => p.Name == "이민준");
        if (toDelete != null)
        {
            people.Remove(toDelete);
            await DS.SavePeopleAsync(people);
            Console.WriteLine("\n 이민준 삭제 후 저장완료");
            
        }
    }
}