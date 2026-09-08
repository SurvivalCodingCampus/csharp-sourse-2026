namespace Day06_Model_Repository.Models;

public class Item(int itemId = 0, string name = "", int count = 1)
{
    public int ItemId { get; set; } = itemId;
    public string Name { get; set; } = name;
    
    // Item 수량 지정시 제한
    public int Count { get; set; } = count > 0 ? count : throw new Exception("Item의 수량은 1개 이상이어야함.");

    protected bool Equals(Item other)
    {
        return ItemId == other.ItemId && Name == other.Name && Count == other.Count;
    }

    public override string ToString()
    {
        return  $"아이템 이름 : {Name} ( id : {ItemId}, count : {Count})";
    }
}