using Microsoft.AspNetCore.Mvc.ModelBinding;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.ModelBinding;

public class ParsablePrimitiveModelBinder<TModel, TPrimitive> : IModelBinder
    where TModel : IStronglyTypedPrimitive<TModel, TPrimitive>
    where TPrimitive : IParsable<TPrimitive>
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (!TPrimitive.TryParse(value, null, out var primitiveValue))
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"Value for {modelName} must be of type {nameof(TPrimitive)}");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(TModel.Wrap(primitiveValue));
        return Task.CompletedTask;
    }
}