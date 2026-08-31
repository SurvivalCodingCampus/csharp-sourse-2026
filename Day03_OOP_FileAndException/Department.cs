namespace Day03_OOP_FileAndException;

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