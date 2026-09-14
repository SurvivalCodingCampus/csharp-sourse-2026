namespace Day06_Repository;

public class MockItemDataSource : IItemDataSource
{
    public List<Item> Items { get; private set; }

    public MockItemDataSource(List<Item>? initialItems = null)
    {
        Items = initialItems ?? new List<Item>();
    }

    public Task<List<Item>> LoadAllItemsAsync()
    {
        var copy = Items.Select(item => new Item { Id = item.Id, Name = item.Name, Count = item.Count }).ToList();
        return Task.FromResult(copy);
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        Items = items.Select(item => new Item { Id = item.Id, Name = item.Name, Count = item.Count }).ToList();
        return Task.CompletedTask;
    }
}

public class InventoryRepositoryTests
{
    [Test]
    public async Task 테스트케이스1_인벤토리_초기화_및_로드()
    {
        // Given: MockItemDataSource에 "Sword"와 "Shield"가 준비되어 있습니다.
        var mockDataSource = new MockItemDataSource(new List<Item>
        {
            new Item { Id = "1", Name = "Sword", Count = 1 },
            new Item { Id = "2", Name = "Shield", Count = 1 }
        });
        var repository = new InventoryRepository(mockDataSource, maxSlot: 10, maxStack: 10);

        // When: InventoryRepository를 초기화하고 아이템 목록을 로드합니다.
        var items = await repository.GetItemsAsync();

        // Then: 인벤토리에는 "Sword"와 "Shield" 두 아이템이 포함되어야 합니다.
        Assert.That(items, Has.Count.EqualTo(2));
        Assert.That(items.Any(item => item.Name == "Sword"), Is.True);
        Assert.That(items.Any(item => item.Name == "Shield"), Is.True);
    }

    [Test]
    public async Task 테스트케이스2_새로운_아이템_추가() // 성공 
    {
        // Given: 인벤토리에 "Potion" 아이템이 없습니다. maxSlot에는 여유가 있습니다.
        var mockDataSource = new MockItemDataSource(new List<Item>
        {
            new Item { Id = "1", Name = "Sword", Count = 1 },
            new Item { Id = "2", Name = "Shield", Count = 1 }
        });
        var repository = new InventoryRepository(mockDataSource, maxSlot: 10, maxStack: 10);

        // When: "Potion" 아이템을 인벤토리에 추가합니다.
        var result = await repository.AddItemAsync(new Item { Id = "3", Name = "Potion", Count = 1 });

        // Then: 인벤토리의 총 아이템 종류 수가 3개로 늘어나고, "Potion"이 목록에 있어야 합니다.
        Assert.That(result, Is.True);
        var items = await repository.GetItemsAsync();
        Assert.That(items, Has.Count.EqualTo(3));
        Assert.That(items.Any(item => item.Name == "Potion"), Is.True);
    }

    [Test]
    public async Task 테스트케이스3_기존_아이템_개수_증가() // 성공
    {
        // Given: "Sword" 아이템이 인벤토리에 1개 있습니다. maxStack은 20입니다.
        var mockDataSource = new MockItemDataSource(new List<Item>
        {
            new Item { Id = "1", Name = "Sword", Count = 1 }
        });
        var repository = new InventoryRepository(mockDataSource, maxSlot: 10, maxStack: 20);

        // When: "Sword" 아이템을 다시 1개 추가합니다.
        var result = await repository.AddItemAsync(new Item { Id = "1", Name = "Sword", Count = 1 });

        // Then: "Sword" 아이템의 개수가 2개로 증가해야 합니다.
        Assert.That(result, Is.True);
        var sword = await repository.GetItemByIdAsync("1");
        Assert.That(sword, Is.Not.Null);
        Assert.That(sword!.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task 테스트케이스4_새로운_아이템_추가_() // 실패_maxSlot_초과
    {
        // Given: 인벤토리의 maxSlot이 2이며, 현재 "Sword"와 "Shield" 두 아이템이 있습니다.
        var mockDataSource = new MockItemDataSource(new List<Item>
        {
            new Item { Id = "1", Name = "Sword", Count = 1 },
            new Item { Id = "2", Name = "Shield", Count = 1 }
        });
        var repository = new InventoryRepository(mockDataSource, maxSlot: 2, maxStack: 10);

        // When: "Potion" 아이템을 인벤토리에 추가하려 시도합니다.
        var result = await repository.AddItemAsync(new Item { Id = "3", Name = "Potion", Count = 1 });

        // Then: AddItemAsync 메서드는 false를 반환해야 하며, "Potion"은 인벤토리 목록에 추가되지 않아야 합니다.
        Assert.That(result, Is.False);
        var items = await repository.GetItemsAsync();
        Assert.That(items.Any(item => item.Name == "Potion"), Is.False);
    }

    [Test]
    public async Task 테스트케이스5_아이템_개수_증가()  // 실패_maxStack_초과
    {
        // Given: "Potion" 아이템이 인벤토리에 99개 있습니다. maxStack은 99입니다.
        var mockDataSource = new MockItemDataSource(new List<Item>
        {
            new Item { Id = "1", Name = "Potion", Count = 99 }
        });
        var repository = new InventoryRepository(mockDataSource, maxSlot: 10, maxStack: 99);

        // When: "Potion" 아이템을 다시 1개 추가하려 시도합니다.
        var result = await repository.AddItemAsync(new Item { Id = "1", Name = "Potion", Count = 1 });

        // Then: AddItemAsync 메서드는 false를 반환해야 하며, "Potion"의 개수는 여전히 99개여야 합니다.
        Assert.That(result, Is.False);
        var potion = await repository.GetItemByIdAsync("1");
        Assert.That(potion, Is.Not.Null);
        Assert.That(potion!.Count, Is.EqualTo(99));
    }
}
