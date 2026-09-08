using System.Text.Json;
using Day06_Model_Repository.DataSources;
using Day06_Model_Repository.Interfaces;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Tests;

public class MockItemDataSource : IItemDataSource
{
    private const int SwordId = 1;
    private const int ShieldId = 2;
    private const int PotionId = 10;
    
    Item sword = new Item(SwordId, "Sword");
    Item shield = new Item(ShieldId, "Shield");
    Item potion = new Item(PotionId, "Potion");
    

    private readonly string _path = "MockItemDataSource.json";
    
  
    public Task<List<Item>> LoadAllItemsAsync()
    {
        ItemDataSource db = new ItemDataSource(_path);
        return db.LoadAllItemsAsync();
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        ItemDataSource db = new ItemDataSource(_path);
        return db.SaveAllItemsAsync(items);
    }

    // SetUp(필요시 아이템을 배열로 삽입)
    private async Task SetUp(Item[]? setUpItems = null)
    {
        IItemDataSource db = this;
        
        // 빈 Json 파일을 생성하기 위한 비어있는 배열 생성
        List<Item> items = new List<Item>() {  };
        
        // 필요시 아이템이 있는 인벤토리 Json을 만들기 위함
        if (setUpItems != null)
        {
            items.AddRange(setUpItems);
        }

        await db.SaveAllItemsAsync(items);
    }
    
    public async Task TestCase1()
    {
        // Given
        // sword와 shield를 가지고 있는 인벤토리 생성
        await SetUp([sword, shield]);
        
        InventoryRepository inventory = new InventoryRepository(this, 2, 20);
        
        string jsonString = JsonSerializer.Serialize(await inventory.GetItemsAsync());

        Console.WriteLine("인벤토리 초기화 및 로드");
        Console.WriteLine(jsonString);
        Console.WriteLine("======================");
    }
    
    public async Task TestCase2()
    {
        // Given
        // Potion을 가지지 않은 maxSlot의 여유를 가진 인벤토리 생성
        InventoryRepository inventory = new InventoryRepository(this, 3, 20);
        await SetUp([sword, shield]);

        await inventory.AddItemAsync(potion);
        string jsonString = JsonSerializer.Serialize(LoadAllItemsAsync().Result);
        
        Console.WriteLine("새로운 아이템 (Potion) 추가 성공");
        Console.WriteLine(jsonString);
        Console.WriteLine("======================");
        
    }
    
    public  async Task TestCase3()
    {
        // Given
        // 기존에 sword 아이템을 가지고 있는 인벤토리 생성
        InventoryRepository inventory = new InventoryRepository(this, 3, 20);
        await SetUp([sword]);

        
        Console.WriteLine("기존 Sword 수량 확인");
        string jsonString = JsonSerializer.Serialize(LoadAllItemsAsync().Result);
        Console.WriteLine(jsonString);

        Console.WriteLine("Sword 수량 추가");
        await inventory.AddItemAsync(sword);
        jsonString = JsonSerializer.Serialize(LoadAllItemsAsync().Result);
        Console.WriteLine(jsonString);
        Console.WriteLine("======================");
    }
    
    public async Task TestCase4()
    {
        // Given
        // 인벤토리 슬롯 2개 제한 후 sword와 shield를 보유 중인 상태
        InventoryRepository inventory = new InventoryRepository(this, 2, 20);
        await SetUp([sword, shield]);
        
        // When
        // 가득 찬 인벤토리에 아이템 넣기 시도
        // 성공시 True, 실패시 False 반환
        bool check = await inventory.AddItemAsync(potion);
        string jsonString = JsonSerializer.Serialize(LoadAllItemsAsync().Result);
        Console.WriteLine("성공 여부 : "+check);
        Console.WriteLine(jsonString);
        Console.WriteLine("======================");
    }

    public async Task TestCase5()
    {
        // Given
        // 이미 최대 수량의 Potion을 가진 인벤토리 생성
        InventoryRepository inventory = new InventoryRepository(this, 3, 99);
        Item potion99 = new Item(PotionId, "Potion", 99);
        await SetUp([sword, shield, potion99]);
        
        // When
        // 최대 수량인 Potion에 1개 더 추가를 시도
        bool checkOverflow = await inventory.AddItemAsync(potion);
        
        // Then
        string jsonString = JsonSerializer.Serialize(LoadAllItemsAsync().Result);
        Console.WriteLine("성공 여부 : "+ checkOverflow);
        Console.WriteLine(jsonString);
        Console.WriteLine("======================");
    }
}