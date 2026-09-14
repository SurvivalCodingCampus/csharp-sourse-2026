namespace Day06_Repository;

public interface IItemDataSource
{
    Task<List<Item>> LoadAllItemsAsync();
    Task SaveAllItemsAsync(List<Item> items);
}
