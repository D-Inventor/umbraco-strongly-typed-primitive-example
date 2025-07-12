using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.Json;

internal class StronglyTypedPrimitiveJsonConverterFactory : JsonConverterFactory
{
    private readonly Dictionary<Type, Type> _converterTypeMap = new()
    {
        { typeof(int), typeof(StronglyTypedIntJsonConverter<>) },
        { typeof(string), typeof(StronglyTypedStringJsonConverter<>) },
        { typeof(Guid), typeof(StronglyTypedGuidJsonConverter<>) }
    };

    public override bool CanConvert(Type typeToConvert)
    {
        var @interface = GetStronglyTypedPrimitiveInterface(typeToConvert);
        return @interface != null && _converterTypeMap.ContainsKey(GetPrimitiveType(@interface));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var @interface = GetStronglyTypedPrimitiveInterface(typeToConvert);
        if (@interface is null) return null;

        if (!_converterTypeMap.TryGetValue(GetPrimitiveType(@interface), out var converterType)) return null;

        var completeType = converterType.MakeGenericType(typeToConvert);
        var converter = Activator.CreateInstance(
            completeType,
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: [],
            culture: null) as JsonConverter;

        return converter;
    }

    private static Type? GetStronglyTypedPrimitiveInterface(Type typeToConvert)
        => typeToConvert
        .GetInterfaces()
        .SingleOrDefault(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IStronglyTypedPrimitive<,>));

    private static Type GetPrimitiveType(Type @interface)
        => @interface.GetGenericArguments()[1];
}