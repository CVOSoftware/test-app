using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Host.API.Middlewares;

namespace TestApp.Host.API.Extensions;

public static class IServiceCollectionExtension
{
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IServiceCollectionExtension).Assembly);
        services.AddFluentValidationAutoValidation();
    }
}