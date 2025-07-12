using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StronglyTypedPrimitiveExample.Website.Api.Json;

public class ConfigureStronglyTypedPrimitiveJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Insert(0, new StronglyTypedPrimitiveJsonConverterFactory());
    }
}