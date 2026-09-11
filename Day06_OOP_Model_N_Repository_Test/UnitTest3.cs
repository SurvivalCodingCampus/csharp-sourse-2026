using Day06_OOP_Model_N_Repository;
using Day06_OOP_Model_N_Repository.Data.Repositories;
using Day06_OOP_Model_N_Repository.DataSources;
using NUnit.Framework.Internal;

namespace Day06_OOP_Model_N_Repository_Test;

public class Tests3 {

    [Test]
    [Description("테스트 케이스 3. 기존 아이템 개수 증가(성공)")]
    public async Task Test3(){
        //Given
        Tests.MockItemDataSource itemDataSource = new();
        InventoryRepository inventoryRepositoryInfo = new(1, 20, itemDataSource);

        //When
        await inventoryRepositoryInfo.AddItemAsync(new Item("용사3", "Sward", 2));

        //Then
        var items = await itemDataSource.LoadAllItemsAsync(); //불러와
        var swardItem = items.FirstOrDefault(i => i.Name == "Sward"); //Sward개수 2개 맞는지, 변수를 더 만든 이유: 안전하게 확인하고 수량 검사위함  🆘
        Assert.That(swardItem, Is.Not.Null); // Sward가 있나?
        Assert.That(swardItem.Cnt, Is.EqualTo(1)); //Sward 수량 1개 맞는지

    }
}



/*
tl;dr:


*/