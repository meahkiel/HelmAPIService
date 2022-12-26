namespace Core.Common.ValueObjects;

public record PostingObject
{
    public static PostingObject Posted()
    {

        return new PostingObject
        {
            IsPosted = true,
            PostedAt = DateTime.Now
        };
    }

    public static PostingObject Create(bool isPosted, DateTime? postedAt = null)
    {
        return new PostingObject
        {
            IsPosted = isPosted,
            PostedAt = postedAt
        };
    }

    public bool IsPosted { get; init; } = false;
    public DateTime? PostedAt { get; init; } = null;
}