using NUnit.Framework;
using Day06_Model_Repository.Data;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Tests.Data;

[TestFixture]
public class InventoryRepositoryTests
{
    // 테스트 케이스 1. 인벤토리 초기화 및 로드
    [Test]
    public async Task GetItemsAsync_초기데이터가_있으면_전부_로드된다()
    {
        // Given: MockItemDataSource에 "Sword"와 "Shield"가 준비되어 있음
        var mockSource = new MockItemDataSource
        {
            Items = new List<Item>
            {
                new Item(1, "Sword", 1),
                new Item(2, "Shield", 1)
            }
        };
        var repository = new InventoryRepository(mockSource, maxSlot: 10, maxStack: 10);

        // When: InventoryRepository를 초기화하고 아이템 목록을 로드
        List<Item> items = await repository.GetItemsAsync();

        // Then: 인벤토리에는 "Sword"와 "Shield" 두 아이템이 포함되어야 함
        Assert.That(items.Count, Is.EqualTo(2));
        Assert.That(items.Any(i => i.Name == "Sword"), Is.True);
        Assert.That(items.Any(i => i.Name == "Shield"), Is.True);
    }

    // 테스트 케이스 2. 새로운 아이템 추가 (성공)
    [Test]
    public async Task AddItemAsync_새아이템이고_슬롯여유있으면_성공한다()
    {
        // Given: 인벤토리에 "Potion" 아이템이 없음, maxSlot에는 여유가 있음
        var mockSource = new MockItemDataSource
        {
            Items = new List<Item>
            {
                new Item(1, "Sword", 1),
                new Item(2, "Shield", 1)
            }
        };
        var repository = new InventoryRepository(mockSource, maxSlot: 5, maxStack: 10);

        // When: "Potion" 아이템을 인벤토리에 추가
        bool result = await repository.AddItemAsync(new Item(3, "Potion", 1));

        // Then: 총 아이템 종류 수가 3개로 늘어나고, "Potion"이 목록에 있어야 함
        Assert.That(result, Is.True);
        Assert.That(mockSource.Items.Count, Is.EqualTo(3));
        Assert.That(mockSource.Items.Any(i => i.Name == "Potion"), Is.True);
    }

    // 테스트 케이스 3. 기존 아이템 개수 증가 (성공)
    [Test]
    public async Task AddItemAsync_기존아이템이고_maxStack이내면_개수가증가한다()
    {
        // Given: "Sword" 아이템이 인벤토리에 1개 있음, maxStack은 20
        var mockSource = new MockItemDataSource
        {
            Items = new List<Item>
            {
                new Item(1, "Sword", 1)
            }
        };
        var repository = new InventoryRepository(mockSource, maxSlot: 5, maxStack: 20);

        // When: "Sword" 아이템을 다시 1개 추가
        bool result = await repository.AddItemAsync(new Item(1, "Sword", 1));

        // Then: "Sword" 아이템의 개수가 2개로 증가해야 함
        Assert.That(result, Is.True);
        Item sword = mockSource.Items.First(i => i.Id == 1);
        Assert.That(sword.Count, Is.EqualTo(2));
    }

    // 테스트 케이스 4. 새로운 아이템 추가 (실패 - maxSlot 초과)
    [Test]
    public async Task AddItemAsync_새아이템이고_maxSlot초과하면_실패한다()
    {
        // Given: 인벤토리의 maxSlot이 2이며, 현재 "Sword"와 "Shield" 두 아이템이 있음
        var mockSource = new MockItemDataSource
        {
            Items = new List<Item>
            {
                new Item(1, "Sword", 1),
                new Item(2, "Shield", 1)
            }
        };
        var repository = new InventoryRepository(mockSource, maxSlot: 2, maxStack: 10);

        // When: "Potion" 아이템을 인벤토리에 추가하려 시도
        bool result = await repository.AddItemAsync(new Item(3, "Potion", 1));

        // Then: false를 반환해야 하며, "Potion"은 인벤토리 목록에 추가되지 않아야 함
        Assert.That(result, Is.False);
        Assert.That(mockSource.Items.Any(i => i.Name == "Potion"), Is.False);
    }

    // 테스트 케이스 5. 아이템 개수 증가 (실패 - maxStack 초과)
    [Test]
    public async Task AddItemAsync_기존아이템이고_maxStack초과하면_실패한다()
    {
        // Given: "Potion" 아이템이 인벤토리에 99개 있음, maxStack은 99
        var mockSource = new MockItemDataSource
        {
            Items = new List<Item>
            {
                new Item(1, "Potion", 99)
            }
        };
        var repository = new InventoryRepository(mockSource, maxSlot: 5, maxStack: 99);

        // When: "Potion" 아이템을 다시 1개 추가하려 시도
        bool result = await repository.AddItemAsync(new Item(1, "Potion", 1));

        // Then: false를 반환해야 하며, "Potion"의 개수는 여전히 99개여야 함
        Assert.That(result, Is.False);
        Item potion = mockSource.Items.First(i => i.Id == 1);
        Assert.That(potion.Count, Is.EqualTo(99));
    }
}