using Day06_Repository;

namespace Day06_Repository.Demo;

public class InMemoryItemDataSource : IItemDataSource
{
    private List<Item> _items;

    public InMemoryItemDataSource(List<Item> initialItems)
    {
        _items = initialItems;
    }

    public Task<List<Item>> LoadAllItemsAsync()
    {
        var copy = _items.Select(item => new Item { Id = item.Id, Name = item.Name, Count = item.Count }).ToList();
        return Task.FromResult(copy);
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        _items = items.Select(item => new Item { Id = item.Id, Name = item.Name, Count = item.Count }).ToList();
        return Task.CompletedTask;
    }
}
