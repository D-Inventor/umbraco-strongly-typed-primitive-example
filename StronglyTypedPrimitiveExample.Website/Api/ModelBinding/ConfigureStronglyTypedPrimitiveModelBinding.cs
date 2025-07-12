using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StronglyTypedPrimitiveExample.Website.Api.ModelBinding;

public class ConfigureStronglyTypedPrimitiveModelBinding : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new StronglyTypedPrimitiveModelBinderProvider());
    }
}