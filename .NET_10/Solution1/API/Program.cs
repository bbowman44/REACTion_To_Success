using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"ContentRootPath: {builder.Environment.ContentRootPath}");
Console.WriteLine($"CurrentDirectory: {Directory.GetCurrentDirectory()}");
//Console.WriteLine($"appsettings.json exists: {File.Exists(Path.Combine(builder.Environment.ContentRootPath, \"appsettings.json\"))}")

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

var environment = app.Configuration.GetValue<string>("Environment");

if (environment == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

