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

public readonly record struct PersonID(Guid Value) : IStronglyTypedPrimitive<PersonID, Guid>
{
    public static Guid Unwrap(PersonID value) => value.Value;

    public static PersonID Wrap(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}