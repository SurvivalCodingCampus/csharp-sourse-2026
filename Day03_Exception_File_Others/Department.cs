namespace Day03_예외파일조작데이터식;

public class Department
{
    public string Name { get; }
    public Employee Leader { get; }

    public Department(string name, Employee leader)
    {
        Name = name;
        this.Leader = leader;
    }
}