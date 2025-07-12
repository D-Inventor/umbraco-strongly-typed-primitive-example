using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.ModelBinding;

public class StronglyTypedPrimitiveModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var modelType = context.Metadata.ModelType;
        var @interface = GetStronglyTypedPrimitiveInterface(modelType);
        if (@interface is null) return null;

        var primitiveType = GetPrimitiveType(@interface);
        if (primitiveType == typeof(string)) return CreateStringPrimitiveModelBinder(modelType);
        if (PrimitiveIsParsable(primitiveType)) return CreateParsablePrimitiveModelBinder(modelType, primitiveType);

        return null;
    }

    private static BinderTypeModelBinder CreateParsablePrimitiveModelBinder(Type modelType, Type primitiveType)
    {
        var modelBinderType = typeof(ParsablePrimitiveModelBinder<,>).MakeGenericType(modelType, primitiveType);
        return new BinderTypeModelBinder(modelBinderType);
    }

    private static BinderTypeModelBinder CreateStringPrimitiveModelBinder(Type modelType)
    {
        var modelBinderType = typeof(StringPrimitiveModelBinder<>).MakeGenericType(modelType);
        return new BinderTypeModelBinder(modelBinderType);
    }

    private static bool PrimitiveIsParsable(Type primitiveType)
        => primitiveType
        .GetInterfaces()
        .Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IParsable<>));

    private static Type? GetStronglyTypedPrimitiveInterface(Type typeToConvert)
        => typeToConvert
        .GetInterfaces()
        .SingleOrDefault(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IStronglyTypedPrimitive<,>));

    private static Type GetPrimitiveType(Type @interface)
        => @interface.GetGenericArguments()[1];
}