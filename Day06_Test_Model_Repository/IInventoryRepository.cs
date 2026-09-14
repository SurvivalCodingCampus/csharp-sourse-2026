namespace Day06_Model_Repository;

public interface IInventoryRepository
{
    public Task<List<Item>> GetItemsAsync(); //모든 아이템 목록을 비동기적으로 가져오는 메서드.
    public Task<Item?> GetItemByldAsync(int ItemId); //특정 아이템을 비동기적으로 검색하는 메서드.
    public Task<bool> AddItemAsync(Item item); //아이템을 인벤토리에 추가하는 메서드. 성공시 true, 실패시 false
}