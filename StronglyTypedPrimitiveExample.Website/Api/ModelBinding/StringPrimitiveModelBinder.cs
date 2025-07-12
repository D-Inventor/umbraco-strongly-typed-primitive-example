using Microsoft.AspNetCore.Mvc.ModelBinding;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api.ModelBinding;

public class StringPrimitiveModelBinder<TModel> : IModelBinder
    where TModel : IStronglyTypedPrimitive<TModel, string>
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

        if (value is null)
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"Value for {modelName} must be non-null");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(TModel.Wrap(value));
        return Task.CompletedTask;
    }
}