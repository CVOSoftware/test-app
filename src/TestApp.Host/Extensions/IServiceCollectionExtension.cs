using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Domain.Abstraction.Core;
using TestApp.Domain.Core;
using TestApp.Host.API.Middlewares;

namespace TestApp.Host.Extensions;

internal static class IServiceCollectionExtension
{
    public static void AddData(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>((provider, options) =>
        {
            using var scope = provider.CreateScope();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Data");

            options.UseNpgsql(connectionString);
        });
    }

    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
    }
}