
namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddle(ILogger<ErrorHandlingMiddle> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        {
            await next.Invoke(context);
        
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error");
        }
    }
}
