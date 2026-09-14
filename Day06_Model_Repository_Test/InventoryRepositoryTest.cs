using Day06_Model_Repository.Models;
using Day06_Model_Repository.Repositories;

namespace Day06_Model_Repository_Test;

public class Tests
{
    [Test]
    public async Task 인벤토리_초기화_및_아이템_불러오기()
    {
        var items = new List<Item>
        {
            new()
            {
                Id = 1,
                Name = "Sword",
                Count = 1
            },
            new()
            {
                Id = 2,
                Name = "Shield",
                Count = 1
            }
        };

        var mockDataSource = new MockItemDataSource(items);

        var repository = new InventoryRepository(
            mockDataSource,
            10,
            20);

        var result = await repository.GetItemsAsync();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(
            result.Any(item => item.Name == "Sword"),
            Is.True);
        Assert.That(
            result.Any(item => item.Name == "Shield"),
            Is.True);
    }

    [Test]
    public async Task 새로운_아이템_추가시_정상적으로_추가()
    {
        var items = new List<Item>
        {
            new()
            {
                Id = 1,
                Name = "Sword",
                Count = 1
            },
            new()
            {
                Id = 2,
                Name = "Shield",
                Count = 1
            }
        };

        var mockDataSource = new MockItemDataSource(items);

        var repository = new InventoryRepository(
            mockDataSource,
            3,
            20);

        var potion = new Item
        {
            Id = 3,
            Name = "Potion",
            Count = 1
        };

        var result = await repository.AddItemAsync(potion);
        var inventory = await repository.GetItemsAsync();

        Assert.That(result, Is.True);
        Assert.That(inventory.Count, Is.EqualTo(3));
        Assert.That(
            inventory.Any(item => item.Name == "Potion"),
            Is.True);
    }

    [Test]
    public async Task 기존_아이템_추가시_개수가_증가()
    {
        var items = new List<Item>
        {
            new()
            {
                Id = 1,
                Name = "Sword",
                Count = 1
            }
        };

        var mockDataSource = new MockItemDataSource(items);

        var repository = new InventoryRepository(
            mockDataSource,
            10,
            20);

        var sword = new Item
        {
            Id = 1,
            Name = "Sword",
            Count = 1
        };

        var result = await repository.AddItemAsync(sword);
        var item = await repository.GetItemByIdAsync(1);

        Assert.That(result, Is.True);
        Assert.That(item, Is.Not.Null);
        Assert.That(item!.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task 최대_슬롯_초과시_새로운_아이템이_추가되지_않음()
    {
        var items = new List<Item>
        {
            new()
            {
                Id = 1,
                Name = "Sword",
                Count = 1
            },
            new()
            {
                Id = 2,
                Name = "Shield",
                Count = 1
            }
        };

        var mockDataSource = new MockItemDataSource(items);

        var repository = new InventoryRepository(
            mockDataSource,
            2,
            20);

        var potion = new Item
        {
            Id = 3,
            Name = "Potion",
            Count = 1
        };

        var result = await repository.AddItemAsync(potion);
        var inventory = await repository.GetItemsAsync();

        Assert.That(result, Is.False);
        Assert.That(
            inventory.Any(item => item.Name == "Potion"),
            Is.False);
    }

    [Test]
    public async Task 최대_스택_초과시_아이템_개수가_증가하지_않음()
    {
        var items = new List<Item>
        {
            new()
            {
                Id = 3,
                Name = "Potion",
                Count = 99
            }
        };

        var mockDataSource = new MockItemDataSource(items);

        var repository = new InventoryRepository(
            mockDataSource,
            10,
            99);

        var potion = new Item
        {
            Id = 3,
            Name = "Potion",
            Count = 1
        };

        var result = await repository.AddItemAsync(potion);
        var item = await repository.GetItemByIdAsync(3);

        Assert.That(result, Is.False);
        Assert.That(item, Is.Not.Null);
        Assert.That(item!.Count, Is.EqualTo(99));
    }
}