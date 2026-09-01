namespace Day03_Exception_File;

public class Department
{
    public string Name { get; set; }
    public Employee leader { get; set; }
 
    public Department(string name, Employee leader)
    {
        Name = name;
        this.leader = leader;
    }
}