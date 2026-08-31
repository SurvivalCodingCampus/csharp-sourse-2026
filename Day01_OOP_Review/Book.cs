namespace Day01_OOP_Review;

internal class Book
{
    public string title;

    protected bool Equals(Book other)
    {
        return title == other.title;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Book)obj);
    }

    public override int GetHashCode()
    {
        return title.GetHashCode();
    }
}