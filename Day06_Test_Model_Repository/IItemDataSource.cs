namespace Day06_Model_Repository;

public interface IItemDataSource
{
    //아이템 넣고 저장 하는 일
    public Task<List<Item>> LoadAllItemsAsync(); //아이템 데이터를 읽어오는 메서드
    public Task SaveAllItemsAsync(List<Item> items); //아이템 데이터를 저장하는 메서드
}