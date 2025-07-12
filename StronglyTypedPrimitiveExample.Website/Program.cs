using StronglyTypedPrimitiveExample.Website.Api;
using StronglyTypedPrimitiveExample.Website.Api.Json;
using StronglyTypedPrimitiveExample.Website.Api.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice(mvc =>
    {
        mvc.AddJsonOptions(json =>
        {
            json.JsonSerializerOptions.Converters.Insert(0, new StronglyTypedPrimitiveJsonConverterFactory());
        });
    })
    .AddWebsite()
    .AddComposers()
    .Build();

builder.Services.AddSingleton<MessageRepository>();
builder.Services.ConfigureOptions<ConfigureStronglyTypedPrimitiveOptions>();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
