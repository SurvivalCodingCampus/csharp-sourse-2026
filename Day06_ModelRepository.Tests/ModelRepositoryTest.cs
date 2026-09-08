using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day06_ModelRepository;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day06_ModelRepository.Tests;

[TestClass]
[TestSubject(typeof(InventoryRepository))]
public class ModelRepositoryTest
{
    // 테스트 케이스 1. 인벤토리 초기화 및 로드
    [TestMethod]
    public async Task TestCase1_InitializeAndLoad()
    {
        // Given
        var initialItems = new List<Item>
        {
            new Item(1, "Sword", 1),
            new Item(2, "Shield", 1)
        };
        var mockDataSource = new MockItemDataSource(initialItems);
        var repository = new InventoryRepository(mockDataSource, maxSlot: 10, maxStack: 99);

        // When
        var items = await repository.GetItemsAsync();

        // Then
        Assert.AreEqual(2, items.Count);
        Assert.IsTrue(items.Any(i => i.Name == "Sword"));
        Assert.IsTrue(items.Any(i => i.Name == "Shield"));
    }

    // 테스트 케이스 2. 새로운 아이템 추가 (성공)
    [TestMethod]
    public async Task TestCase2_AddNewItem_Success()
    {
        // Given
        var initialItems = new List<Item>
        {
            new Item(1, "Sword", 1),
            new Item(2, "Shield", 1)
        };
        var mockDataSource = new MockItemDataSource(initialItems);
        var repository = new InventoryRepository(mockDataSource, maxSlot: 5, maxStack: 99);

        // When
        var potion = new Item(3, "Potion", 1);
        bool result = await repository.AddItemAsync(potion);

        // Then
        var items = await repository.GetItemsAsync();
        Assert.IsTrue(result);
        Assert.AreEqual(3, items.Count);
        Assert.IsTrue(items.Any(i => i.Id == 3 && i.Name == "Potion"));
    }

    // 테스트 케이스 3. 기존 아이템 개수 증가 (성공)
    [TestMethod]
    public async Task TestCase3_IncreaseExistingItemCount_Success()
    {
        // Given
        var initialItems = new List<Item>
        {
            new Item(1, "Sword", 1)
        };
        var mockDataSource = new MockItemDataSource(initialItems);
        var repository = new InventoryRepository(mockDataSource, maxSlot: 5, maxStack: 20);

        // When
        var anotherSword = new Item(1, "Sword", 1);
        bool result = await repository.AddItemAsync(anotherSword);

        // Then
        var sword = await repository.GetItemByIdAsync(1);
        Assert.IsTrue(result);
        Assert.IsNotNull(sword);
        Assert.AreEqual(2, sword.Count);
    }

    // 테스트 케이스 4. 새로운 아이템 추가 (실패 - maxSlot 초과)
    [TestMethod]
    public async Task TestCase4_AddNewItem_Fail_MaxSlotExceeded()
    {
        // Given
        var initialItems = new List<Item>
        {
            new Item(1, "Sword", 1),
            new Item(2, "Shield", 1)
        };
        var mockDataSource = new MockItemDataSource(initialItems);
        var repository = new InventoryRepository(mockDataSource, maxSlot: 2, maxStack: 99);

        // When
        var potion = new Item(3, "Potion", 1);
        bool result = await repository.AddItemAsync(potion);

        // Then
        var items = await repository.GetItemsAsync();
        Assert.IsFalse(result);
        Assert.AreEqual(2, items.Count);
        Assert.IsFalse(items.Any(i => i.Id == 3));
    }

    // 테스트 케이스 5. 아이템 개수 증가 (실패 - maxStack 초과)
    [TestMethod]
    public async Task TestCase5_IncreaseItemCount_Fail_MaxStackExceeded()
    {
        // Given
        var initialItems = new List<Item>
        {
            new Item(3, "Potion", 99)
        };
        var mockDataSource = new MockItemDataSource(initialItems);
        var repository = new InventoryRepository(mockDataSource, maxSlot: 5, maxStack: 99);

        // When
        var extraPotion = new Item(3, "Potion", 1);
        bool result = await repository.AddItemAsync(extraPotion);

        // Then
        var potion = await repository.GetItemByIdAsync(3);
        Assert.IsFalse(result);
        Assert.IsNotNull(potion);
        Assert.AreEqual(99, potion.Count);
    }
}