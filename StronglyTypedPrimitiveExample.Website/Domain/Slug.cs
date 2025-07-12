namespace StronglyTypedPrimitiveExample.Website.Domain;

public readonly record struct Slug : IStronglyTypedPrimitive<Slug, string>
{
    public Slug(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Any(char.IsUpper)) throw new ArgumentException("A slug cannot contain uppercase characters", nameof(value));
        if (value.StartsWith('-') || value.EndsWith('-')) throw new ArgumentException("A slug cannot start or end with a dash", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public static string Unwrap(Slug value) => value.Value;

    public static Slug Wrap(string value) => new(value);

    public override string ToString() => Value;
}