using System.Text.Json;
using System.Text.Json.Serialization;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.Json;

public class StronglyTypedGuidJsonConverter<TValue> : JsonConverter<TValue>
    where TValue : IStronglyTypedPrimitive<TValue, Guid>
{
    public override TValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TValue.Wrap(reader.GetGuid());

    public override void Write(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options)
        => writer.WriteStringValue(TValue.Unwrap(value));
}