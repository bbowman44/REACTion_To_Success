using Microsoft.EntityFrameworkCore;
using Twisted.API;
using Twisted.Infrastructure.DBContext;


var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddTwistedServices();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TwistedDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TwistedDB")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TwistedDBContext>();

    //if(!dbContext.Database.CanConnect())
    //{
    //    throw new NotImplementedException("Can't connect to the TwistedDB, please check connection string and all other configurations");
    //}
    //dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();