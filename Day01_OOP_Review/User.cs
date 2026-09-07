namespace Day01_OOP_Review;

public class User
{
    private string _name;
    public string Name
    {
        get
        {
          return _name;
        } 
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Name cannot be empty");
            }
            _name = value;
        }
    }
    public int Age = 0;
}