using System.Net;
using System.Text.Json;
using ContosoAcai.Application;
using PowerPilotChat.Application;

namespace PowerPilotChat.Web.Middlewares;

public class GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            
            var errorJson = JsonSerializer.Serialize(Errors.Unexpected());

            await context.Response.WriteAsync(errorJson);
        }
    }
}