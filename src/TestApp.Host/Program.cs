using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using TestApp.Host.API.Extensions;
using TestApp.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData();
builder.Services.AddDomain();
builder.Services.AddMiddlewares();
builder.Services.AddValidators();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestApp",
        Version = "v1"
    });
});

var app = builder.Build();

app.InitMigrations();

app.UsenExceptionMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocExpansion(DocExpansion.None);
    options.SwaggerEndpoint("v1/swagger.json", "API v1");
});
app.MapControllers();
app.Run();