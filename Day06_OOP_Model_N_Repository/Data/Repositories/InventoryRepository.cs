using Day06_OOP_Model_N_Repository.DataSources;

namespace Day06_OOP_Model_N_Repository.Data.Repositories;

public class InventoryRepository : IInventoryRepository{
    public int maxSloat;
    public int maxStack;
    public IItemDataSource ItemDataSource { get; }

    public InventoryRepository(int maxSloatCopy, int maxStackCopy, IItemDataSource itemDataSource)  {
        this.maxSloat = maxSloatCopy;
        this.maxStack = maxStackCopy;
        this.ItemDataSource = itemDataSource;
    }



    public Task<List<Item>> GetItemsAsync() {
        throw new NotImplementedException();
    }

    public Task<Item?> GetItemByIdAsync(int itemId) {
        throw new NotImplementedException();
    }

    //2. 마지막 2개 줄의 조건을 다 완성 못함 ❌❌❌❌❌
    public Task<bool> AddItemAsync(Item item) {
        var itemDataSource = ItemDataSource.LoadAllItemsAsync(); // 아이템 안에 뭐가 있는지 확인
        var containItemDataSource = itemDataSource.;

        try {
            Console.WriteLine("");
        }
        catch {
            return AddItemAsync = null;
        }
    }
}