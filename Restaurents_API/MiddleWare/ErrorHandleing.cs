using Restaurents_API.Exceptions;

public class ErrorHandleing(ILogger<ErrorHandleing> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { error = notFound.Message });

            logger.LogWarning(notFound, notFound.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = "Something went wrong" });

            logger.LogError(ex, ex.Message);
        }
    }
}
