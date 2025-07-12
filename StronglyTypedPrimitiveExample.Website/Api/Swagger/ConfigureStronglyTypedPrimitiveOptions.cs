using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StronglyTypedPrimitiveExample.Website.Api.Swagger;

public class ConfigureStronglyTypedPrimitiveOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SchemaFilter<StronglyTypedPrimitiveSchemaFilter>();
    }
}