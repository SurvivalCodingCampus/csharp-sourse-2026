namespace Day06_Model_Repository.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    
    public Item(int id, string name, int count)
    {
        Id = id;
        Name = name;
        Count = count;
    }
}