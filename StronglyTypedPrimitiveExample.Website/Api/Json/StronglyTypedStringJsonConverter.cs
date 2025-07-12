using System.Text.Json;
using System.Text.Json.Serialization;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.Json;

public class StronglyTypedStringJsonConverter<TValue> : JsonConverter<TValue>
    where TValue : IStronglyTypedPrimitive<TValue, string>
{
    public override TValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TValue.Wrap(reader.GetString() ?? throw new JsonException($"Null-value was unexpected for {nameof(TValue)}"));

    public override void Write(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options)
        => writer.WriteStringValue(TValue.Unwrap(value));
}