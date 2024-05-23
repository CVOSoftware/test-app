using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Host.API.Middlewares;

namespace TestApp.Host.Extensions;

internal static class WebApplicationExtension
{
    public static void InitMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dataContext.Database.Migrate();
    }

    public static void UsenExceptionMiddleware(this WebApplication app)
        => app.UseMiddleware<ExceptionMiddleware>();
}