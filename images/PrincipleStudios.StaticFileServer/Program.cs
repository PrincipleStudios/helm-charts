using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

#pragma warning disable ASP0013 // suprress suggestion to use
builder.Host.ConfigureAppConfiguration((context, config) => {
    foreach (var s in config.Sources.OfType<FileConfigurationSource>())
    {
        // s.ReloadOnChange = false;
    }
});
#pragma warning restore ASP0013

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.GetTypedHeaders().CacheControl =
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                MustRevalidate = true,
                Public = true,
            };
    }
});

app.Run();