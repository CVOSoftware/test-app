using Microsoft.AspNetCore.Http;
using TestApp.Domain.Abstraction.Exceptions;

namespace TestApp.Host.API.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DomainException exception) when (DomainException.NotFound(exception))
        {
            var result = Results.Problem(statusCode: StatusCodes.Status404NotFound);

            await result.ExecuteAsync(context);
        }
        catch (DomainException exception)
        {
            var result = Results.Problem(statusCode: StatusCodes.Status422UnprocessableEntity, detail: exception.Message);

            await result.ExecuteAsync(context);
        }
        catch (Exception exception)
        {
            var result = Results.Problem(statusCode: StatusCodes.Status500InternalServerError, detail: exception.Message);

            await result.ExecuteAsync(context);
        }
    }
}