using Day06_OOP_Model_N_Repository;
using Day06_OOP_Model_N_Repository.Data.Repositories;
using Day06_OOP_Model_N_Repository.DataSources;
using NUnit.Framework.Internal;

namespace Day06_OOP_Model_N_Repository_Test;

public class Tests2 {

    [Test]
    [Description("테스트 케이스 2. 새로운 아이템 추가(성공)")]
    public async Task Test2(){
        //Given
        //Tests.: UnitTest1.cs에 있는 Test2에 있는 목아이템데이타를 불러와 쓰겠다는 의미
        //가짜 데이터 소스(목) 생성
        //인벤토리 생성
        //ㄴ최대 슬롯: 3,  슬롯당 최대 중첩 스택: 9, itemDataSource에 대한 내용을 쓸거란 의미
        Tests.MockItemDataSource itemDataSource = new();
        InventoryRepository inventoryRepositoryInfo = new(3, 9, itemDataSource);

        //when 아이템 추가
        //await: 내 뒤에 작성된 함수 작업 끝날때까지 기달
        //📌inventoryRepositoryInfo.AddItemAsync();가 끝나야 📌다음 함수가 실행할 수 있는 구조
        // 인스턴스는 = new(); 해야하지만 한줄로 쓰면 이렇게씀
        await inventoryRepositoryInfo.AddItemAsync(new Item("용사2", "Potion", 1));

        //Then
        //await를 쓴 이유: 불러올때까지 기다리게 하기 위해
        //앞에서도 코드의 틀이 비동기이기 때문에 여기서도 마찬가지로 async를 해야함
        //await를 안 쓰면 그냥 상자(Task)가 됨 =====> 즉 올려놓기만 하고 사용되지는 않을 함수가 될 수 있다.
        //Asset: 
        //That: 검증할 대상 이 이거다 라는 목적으로 쓰임
        //Any: 목록중에 조건에 맞는 항목이 있는지 확인 목적으로 
        var items = await itemDataSource.LoadAllItemsAsync();
        Assert.That(items.Count, Is.EqualTo(2));
        Assert.That(items.Any(i => i.Name == "Potion"), Is.False); //담겼으면 거짓으로 간주
    }
}


/*
tl;dr: 아이템을 비동기로 넣고, 몇개 넣어졌는지 확인, 그 목록의 아이템 이름이 맞는지 확인


*/