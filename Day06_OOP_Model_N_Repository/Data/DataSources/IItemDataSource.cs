namespace Day06_OOP_Model_N_Repository.DataSources;

public interface IItemDataSource {
    //반환 타입있는 비동기 메서드
    Task<List<Item>> LoadAllItemsAsync();
    
    //모든 비동기 아이템을 저장 함수
    Task SaveAllItemAsync(List<Item> items); //item data save method
}