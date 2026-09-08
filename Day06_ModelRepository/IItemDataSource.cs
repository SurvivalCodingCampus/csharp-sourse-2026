namespace Day06_ModelRepository;

public interface IItemDataSource
{
    Task<List<Item>> LoadAllItemsAsync();
    Task SaveAllItemsAsync(List<Item> items);
}