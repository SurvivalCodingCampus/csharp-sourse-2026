namespace Day06_Model_Repository;

public class Item 
{
    //아이템, 이름, 개수 변경 동작
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }

    public Item(int id, string name, int count)
    {
        Id = id;
        Name = name;
        Count = count;
    }
    public void IncreaseCount(int amount)
    {
        Count += amount;
    }
}