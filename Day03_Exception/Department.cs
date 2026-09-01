namespace Day03_Exception;

public class Department
{
    public string Name { get; }
    public Employee leader { get; }

    public Department(string name, Employee leader)
    {
        Name = name;
        this.leader = leader;
    }
}