using Microsoft.OpenApi.Models;
using StronglyTypedPrimitiveExample.Website.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StronglyTypedPrimitiveExample.Website.Api.Swagger;

public class StronglyTypedPrimitiveSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<Type, (string type, string? format)> _primitiveTypeMap = new()
    {
        [typeof(int)] = (OpenApiTypes.Int, null),
        [typeof(string)] = (OpenApiTypes.String, null),
        [typeof(Guid)] = (OpenApiTypes.Guid, "uuid")
    };

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var @interface = GetStronglyTypedPrimitiveInterface(context.Type);
        if (@interface is null || !_primitiveTypeMap.TryGetValue(GetPrimitiveType(@interface), out var schemaType)) return;

        schema.Properties.Clear();
        schema.Required.Clear();
        schema.OneOf.Clear();
        schema.AdditionalPropertiesAllowed = true;
        schema.Type = schemaType.type;
        schema.Format = schemaType.format;
    }

    private static Type? GetStronglyTypedPrimitiveInterface(Type type)
        => type
        .GetInterfaces()
        .SingleOrDefault(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IStronglyTypedPrimitive<,>));

    private static Type GetPrimitiveType(Type @interface)
        => @interface.GetGenericArguments()[1];

    private static class OpenApiTypes
    {
        public const string Int = "integer";
        public const string String = "string";
        public const string Guid = String;
    }
}