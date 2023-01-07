namespace Core.Common.ValueObjects;

public record PostingObject
{
    public static PostingObject Post()
    {

        return new PostingObject
        {
            IsPosted = true,
            PostedAt = DateTime.Now
        };
    }

    public static PostingObject UnPost()
    {

        return new PostingObject
        {
            IsPosted = false,
            PostedAt = null
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

    public bool IsPosted { get; private set; } = false;
    public DateTime? PostedAt { get; private set; } = null;


}