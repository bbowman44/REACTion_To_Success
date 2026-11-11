using Microsoft.EntityFrameworkCore;
using Twisted.API;
using Twisted.Infrastructure.DBContext;
using Microsoft.OpenApi;
using System.Reflection.Metadata;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTwistedServices();

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Describe the key: a header called Twisted-Key
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["TwistedKey"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Name = "Twisted-Key",
            Description = "Add the API key here. It will be sent as the Twisted-Key header."
        };

        // Tell Swagger UI to send that key with every request
        document.Security = [new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("TwistedKey", document)] = []
        }];

        return Task.CompletedTask;
    });
});

builder.Services.AddDbContext<TwistedDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TwistedDB")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TwistedDBContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can't connect to the TwistedDB, please check connection string and all other configurations");
    }
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.EnablePersistAuthorization();
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();