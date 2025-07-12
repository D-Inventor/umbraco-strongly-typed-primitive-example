using System.Diagnostics.CodeAnalysis;

namespace StronglyTypedPrimitiveExample.Website.Domain;

public class MessageEntity
{
    private MessageEntity()
    {
    }

    public MessageID Id { get; set; }

    public required string Title { get; init; }

    public PersonID AuthorId { get; init; }

    public static MessageEntity Create(string title, PersonID authorId)
        => new() { Title = title, AuthorId = authorId };
}

public readonly record struct MessageID(Guid Value) : IParsable<MessageID>, IStronglyTypedPrimitive<MessageID, Guid>
{
    public static MessageID Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result
        : throw new ArgumentException("Cannot parse input into MessageID", nameof(s));

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MessageID result)
    {
        (var success, result) = Guid.TryParse(s, provider, out var guidResult) ? (true, new MessageID(guidResult))
            : (false, default);

        return success;
    }

    public static Guid Unwrap(MessageID value) => value.Value;

    public static MessageID Wrap(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}