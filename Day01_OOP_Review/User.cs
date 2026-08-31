namespace Day01_OOP_Review;

public class User
{
    public int Age = 0;
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (value.Length < 3) throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }

    g
}