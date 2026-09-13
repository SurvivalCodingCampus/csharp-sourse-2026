using Day06_OOP_Model_N_Repository;
using Day06_OOP_Model_N_Repository.Data.Repositories;
using Day06_OOP_Model_N_Repository.DataSources;
using NUnit.Framework.Internal;

namespace Day06_OOP_Model_N_Repository_Test;

public class Tests4 {

    [Test]
    [Description("테스트 케이스 4.새로운 아이템 추가 (실패 - maxSlot 초과)")]
    public async Task Test4(){
        //Given
        // maxSlot 2개만 담을 수 있는 곳에  ---> 아이템(Sward, Shield) 2가 있는 상태
        Tests.MockItemDataSource itemDataSource = new();
        InventoryRepository inventoryRepositoryInfo = new(2, 20, itemDataSource);
        
        await inventoryRepositoryInfo.AddItemAsync(new Item("용사4", "Sward", 1));
        await inventoryRepositoryInfo.AddItemAsync(new Item("용사4", "Shield", 1));
        
        //When
        //아이템이 꽉 찬곳에 Potion을 추가
        // 성공 실패를 확인하기 위해 변수 하나 더 만듬  ---> asset에서 검사해서 T/F를 가림
        bool isSuccess = await inventoryRepositoryInfo.AddItemAsync(new Item("용사4", "Potion", 1));

        //Then
        var items = await itemDataSource.LoadAllItemsAsync(); //불러와
        //Assert.That(items.Any(i => i.Name == "Potion"), Is.False);
        Assert.That(isSuccess, Is.False);

    }
}



/*
tl;dr:


*/