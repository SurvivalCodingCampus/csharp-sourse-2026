using Day06_OOP_Model_N_Repository;
using Day06_OOP_Model_N_Repository.Data.Repositories;
using Day06_OOP_Model_N_Repository.DataSources;
using NUnit.Framework.Internal;

namespace Day06_OOP_Model_N_Repository_Test;

public class Tests5 {

    [Test]
    [Description("테스트 케이스 5.아이템 개수 증가 (실패 - maxStack 초과)")]
    public async Task Test5(){
        //Given
        Tests.MockItemDataSource itemDataSource = new();
        InventoryRepository inventoryRepositoryInfo = new(1, 99, itemDataSource);
        
        await inventoryRepositoryInfo.AddItemAsync(new Item("용사5", "Potion", 99));
        
        //when
        bool isSuccess = await inventoryRepositoryInfo.AddItemAsync(new Item("용사4", "Potion", 1));
        
        //Then 
        Assert.That(isSuccess, Is.False);
    }
}


/*
tl;dr: 같은 이름을 2번 저장하려할때 false 


*/