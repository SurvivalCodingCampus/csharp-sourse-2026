using Day06_OOP_Model_N_Repository;
using Day06_OOP_Model_N_Repository.Data.Repositories;
using Day06_OOP_Model_N_Repository.DataSources;

namespace Day06_OOP_Model_N_Repository_Test;

public class Tests {
    public class MockItemDataSource : IItemDataSource {

        private List<Item> _items = new List<Item>
        {
            new Item("용사1", "Sward", 1),
            new Item("용사2", "Shield", 1)
        };
        
        //정의가 안되있을때 아래 문구가 적혀있어야되는데
        // 정의를 하기 위해서
        // 정의
        public Task<List<Item>> LoadAllItemsAsync() {
            //throw new NotImplementedException();
            return Task.FromResult(_items); //나중에 결과를 넣어주겠다 의미
        }
        public Task SaveAllItemAsync(List<Item> items) {
            //throw new NotImplementedException();
            _items = items; //넣어주기 위해 
            return Task.CompletedTask; //이미 완료된 Task라는 뜻
        }
        
    }

    [SetUp]
    public void Setup() {
       
    }

    [Test]
    [Description("테스트 케이스 1. 인벤토리 초기화 및 로드")]
    public async Task Test1() {
        //Given
        MockItemDataSource itemDataSource = new MockItemDataSource();
        
        // itemDataSource인스턴스한 변수이름을 넣어서 부모와 자기 자신 모두 불러와 정보를 넣겠다는 의미로 아래 넣음
        //초기화한 로직
        InventoryRepository inventoryRepositoryInfo = new InventoryRepository(3, 9, itemDataSource );
        
        //When
        var items = await itemDataSource.LoadAllItemsAsync();
        
        //Then
        Assert.That(items[0].Name, Is.EqualTo("Sward"));
        Assert.That(items[1].Name, Is.EqualTo("Shield"));
    }
    
    
}


