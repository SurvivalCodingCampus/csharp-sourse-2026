using System.Text.Json;
using Day06_Repository;
using Day06_Repository.Demo;

var jsonOptions = new JsonSerializerOptions();

async Task PrintItemsAsync(InventoryRepository repository)
{
    var items = await repository.GetItemsAsync();
    Console.WriteLine(JsonSerializer.Serialize(items, jsonOptions));
}

void PrintSeparator()
{
    Console.WriteLine(new string('=', 24));
}

// 시나리오 1: 인벤토리 초기화 및 로드
Console.WriteLine("인벤토리 초기화 및 로드");
var dataSource1 = new InMemoryItemDataSource(new List<Item>
{
    new Item { Id = "1", Name = "Sword", Count = 1 },
    new Item { Id = "2", Name = "Shield", Count = 1 }
});
var repository1 = new InventoryRepository(dataSource1, maxSlot: 5, maxStack: 10);
await PrintItemsAsync(repository1);
PrintSeparator();

// 시나리오 2: 새로운 아이템(Potion) 추가 성공
var dataSource2 = new InMemoryItemDataSource(new List<Item>
{
    new Item { Id = "1", Name = "Sword", Count = 1 },
    new Item { Id = "2", Name = "Shield", Count = 1 }
});
var repository2 = new InventoryRepository(dataSource2, maxSlot: 5, maxStack: 10);
var addPotionResult = await repository2.AddItemAsync(new Item { Id = "3", Name = "Potion", Count = 1 });
Console.WriteLine(addPotionResult ? "새로운 아이템 (Potion) 추가 성공" : "새로운 아이템 (Potion) 추가 실패");
await PrintItemsAsync(repository2);
PrintSeparator();

// 시나리오 3: 기존 Sword 수량 확인 후 증가
var dataSource3 = new InMemoryItemDataSource(new List<Item>
{
    new Item { Id = "1", Name = "Sword", Count = 1 }
});
var repository3 = new InventoryRepository(dataSource3, maxSlot: 5, maxStack: 20);
Console.WriteLine("기존 Sword 수량 확인");
await PrintItemsAsync(repository3);
await repository3.AddItemAsync(new Item { Id = "1", Name = "Sword", Count = 1 });
Console.WriteLine("Sword 수량 추가");
await PrintItemsAsync(repository3);
PrintSeparator();

// 시나리오 4: 새로운 아이템 추가 실패 (maxSlot 초과)
var dataSource4 = new InMemoryItemDataSource(new List<Item>
{
    new Item { Id = "1", Name = "Sword", Count = 1 },
    new Item { Id = "2", Name = "Shield", Count = 1 }
});
var repository4 = new InventoryRepository(dataSource4, maxSlot: 2, maxStack: 10);
var addOverSlotResult = await repository4.AddItemAsync(new Item { Id = "3", Name = "Potion", Count = 1 });
Console.WriteLine($"성공 여부 : {addOverSlotResult}");
await PrintItemsAsync(repository4);
PrintSeparator();

// 시나리오 5: 아이템 개수 증가 실패 (maxStack 초과)
var dataSource5 = new InMemoryItemDataSource(new List<Item>
{
    new Item { Id = "1", Name = "Sword", Count = 1 },
    new Item { Id = "2", Name = "Shield", Count = 1 },
    new Item { Id = "3", Name = "Potion", Count = 99 }
});
var repository5 = new InventoryRepository(dataSource5, maxSlot: 5, maxStack: 99);
var addOverStackResult = await repository5.AddItemAsync(new Item { Id = "3", Name = "Potion", Count = 1 });
Console.WriteLine($"성공 여부 : {addOverStackResult}");
await PrintItemsAsync(repository5);
PrintSeparator();
