namespace Day03_Exception_File;

public class Department
{
    public string DeptName { get; }
    public Empolyee Name { get; }

    public Department(string deptName, Empolyee name)
    {
        DeptName = deptName;
        this.Name = Name;
    }
    
    public string Name { get;  }
    public Employee Leader { get;  }

    public Department(string name, Employee leader)
    {
        Name = name;
        this.Leader = leader;
    }
    
   

}