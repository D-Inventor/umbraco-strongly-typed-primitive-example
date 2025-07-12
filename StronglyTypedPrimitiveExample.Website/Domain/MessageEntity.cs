namespace StronglyTypedPrimitiveExample.Website.Domain;

public class MessageEntity
{
    private MessageEntity()
    {
    }

    public MessageID Id { get; set; }

    public required Slug Slug { get; init; }

    public required string Title { get; init; }

    public PersonID AuthorId { get; init; }

    public static MessageEntity Create(string title, Slug slug, PersonID authorId)
        => new() { Title = title, Slug = slug, AuthorId = authorId };
}

public readonly record struct MessageID(Guid Value) : IStronglyTypedPrimitive<MessageID, Guid>
{
    public static Guid Unwrap(MessageID value) => value.Value;

    public static MessageID Wrap(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}