namespace Day01_OOP_Review;

public class Book
{
    public required string Title { get; init; }
    public required DateOnly PublishedOn { get; init; }
    public override bool Equals(object? obj)
    {
        return obj is Book other
               && Title == other.Title
               && PublishedOn == other.PublishedOn;
    }
    public override int GetHashCode()
        => HashCode.Combine(Title, PublishedOn);
}