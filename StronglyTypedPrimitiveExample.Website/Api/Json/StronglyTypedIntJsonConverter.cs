using System.Text.Json;
using System.Text.Json.Serialization;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.Json;

public class StronglyTypedIntJsonConverter<TValue> : JsonConverter<TValue>
    where TValue : IStronglyTypedPrimitive<TValue, int>
{
    public override TValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TValue.Wrap(reader.GetInt32());

    public override void Write(Utf8JsonWriter writer, TValue value, JsonSerializerOptions options)
        => writer.WriteNumberValue(TValue.Unwrap(value));
}