using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Application;

namespace PowerPilotChat.Web.Extensions;

public static class ActionResultExtensions
{
    public static IActionResult ToApiResponse<T>(this Result<T> result)
    {
        return result.IsSuccess ? 
            new OkObjectResult(result.Value) 
            : new BadRequestObjectResult(result.Error);    
    }
    
    public static IActionResult ToApiResponse<T>(this T result)
    {
        return new OkObjectResult(result);
    }
}