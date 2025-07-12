using StronglyTypedPrimitiveExample.Website.Api;
using StronglyTypedPrimitiveExample.Website.Api.Json;
using StronglyTypedPrimitiveExample.Website.Api.ModelBinding;
using StronglyTypedPrimitiveExample.Website.Api.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

builder.Services.AddSingleton<MessageRepository>();
builder.Services.ConfigureOptions<ConfigureStronglyTypedPrimitiveOptions>();
builder.Services.ConfigureOptions<ConfigureStronglyTypedPrimitiveJsonOptions>();
builder.Services.ConfigureOptions<ConfigureStronglyTypedPrimitiveModelBinding>();

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
