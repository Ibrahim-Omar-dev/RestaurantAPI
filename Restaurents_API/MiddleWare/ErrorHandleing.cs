using System.Net;
using System.Text.Json;
using Restaurant.Model.Exceptions;
using Restaurents_API.Exceptions;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException notFound)
        {
            logger.LogWarning(notFound, notFound.Message);
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, notFound.Message);
        }
        catch (ForbidException forbid)
        {
            logger.LogWarning(forbid, "Forbidden access");
            await HandleExceptionAsync(context, HttpStatusCode.Forbidden, "Access forbidden");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            statusCode = (int)statusCode,
            error = message
        };

        var json = JsonSerializer.Serialize(errorResponse);

        return context.Response.WriteAsync(json);
    }
}
