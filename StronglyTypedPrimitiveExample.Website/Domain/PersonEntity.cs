using System.Diagnostics.CodeAnalysis;

namespace StronglyTypedPrimitiveExample.Website.Domain;

public class PersonEntity
{
    private PersonEntity()
    {
    }

    public PersonID Id { get; private set; }

    public required string Name { get; init; }

    public static PersonEntity Create(string name)
        => new() { Name = name };

    public static PersonEntity CreateExisting(PersonID id, string name)
        => new() { Id = id, Name = name };
}

public readonly record struct PersonID(Guid Value) : IParsable<PersonID>, IStronglyTypedPrimitive<PersonID, Guid>
{
    public static PersonID Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result
        : throw new ArgumentException("Cannot parse input into PersonID", nameof(s));

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PersonID result)
    {
        (var success, result) = Guid.TryParse(s, provider, out var guidResult) ? (true, new PersonID(guidResult))
            : (false, default);

        return success;
    }

    public static Guid Unwrap(PersonID value) => value.Value;

    public static PersonID Wrap(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}