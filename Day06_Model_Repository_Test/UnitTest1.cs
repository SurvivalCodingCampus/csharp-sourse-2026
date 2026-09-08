using Day06_Model_Repository.Data.DataSources;
using Day06_Model_Repository.Data.Repositories;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository_Test;

public class ItemTests
{
    public class MockItemDataSource : IItemDataSource
    {
        private List<Item> _itemList = new List<Item>
        {
            new Item(1, "Sword",1),
            new Item(2, "Shield",1)
        };
        
        public async Task<List<Item>> LoadAllItemsAsync()
        {
            return _itemList;
        }

        public async Task SaveAllItemsAsync(List<Item> items)
        {
            _itemList = items;
            await Task.CompletedTask;
        }
    }

    private IItemDataSource _itemDataSource;
    private InventoryRepository _inventory;
    
    [SetUp]
    public void Setup()
    {
        _itemDataSource = new MockItemDataSource();
        _inventory = new InventoryRepository(_itemDataSource, 3, 20);
    }
    
    [Test] // 테스트 케이스 1.인벤토리 초기화 및 로드
    public async Task Test1()
    {
        var items = await _itemDataSource.LoadAllItemsAsync();
        
        Assert.That(items[0].Name, Is.EqualTo("Sword"));
        Assert.That(items[1].Name, Is.EqualTo("Shield"));
    }

    [Test] // 테스트 케이스 2.새로운 아이템 추가 (성공)
    public async Task Test2()
    {
        var items = await _itemDataSource.LoadAllItemsAsync();
        await _inventory.AddItemAsync(new Item(3, "Potion",1));
        
        Assert.That(items[2].Name, Is.EqualTo("Potion"));
        Assert.That(items.Count, Is.EqualTo(3));
    }

    [Test] // 테스트 케이스 3.기존 아이템 개수 증가 (성공)
    public async Task Test3()
    {
        var items = await _itemDataSource.LoadAllItemsAsync();
        
        Assert.That(items[0].Count, Is.EqualTo(1));
        
        await _inventory.AddItemAsync(new Item(1, "Sword",1));
        Assert.That(items[0].Count, Is.EqualTo(2));
    }

    [Test] // 테스트 케이스 4.새로운 아이템 추가 (실패 - maxSlot 초과)
    public async Task Test4()
    {
        _inventory = new InventoryRepository(_itemDataSource, 2, 20);
        var items = await _itemDataSource.LoadAllItemsAsync();
        
        Assert.That(items.Count, Is.EqualTo(2));

        bool isAdd = await _inventory.AddItemAsync(new Item(3, "Potion",1));
        Assert.That(isAdd, Is.False);
        Assert.That(items[0].Name, Is.EqualTo("Sword"));
        Assert.That(items[1].Name, Is.EqualTo("Shield"));
        Assert.That(items.Count, Is.EqualTo(2));
    }
    
    [Test] // 테스트 케이스 5.아이템 개수 증가 (실패 -maxStack 초과)
    public async Task Test5()
    {
        _inventory = new InventoryRepository(_itemDataSource, 3, 99);
        var items = await _itemDataSource.LoadAllItemsAsync();
        
        await _inventory.AddItemAsync(new Item(3, "Potion",99));
        Assert.That(items[2].Count, Is.EqualTo(99));
        
        bool isAdd = await _inventory.AddItemAsync(new Item(3, "Potion",1));
        Assert.That(isAdd, Is.False);
        Assert.That(items[2].Count, Is.EqualTo(99));
    }
}