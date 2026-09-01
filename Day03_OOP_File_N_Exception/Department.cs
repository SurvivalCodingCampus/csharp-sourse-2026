namespace Day03_OOP_FileAndException;
//여러가지 데이터 형식
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