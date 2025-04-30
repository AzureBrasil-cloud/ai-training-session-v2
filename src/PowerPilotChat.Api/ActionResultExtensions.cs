using PowerPilotChat.Application;

namespace PowerPilotChat.Api;

public static class ActionResultExtensions
{
    public static IResult ToApiResponse<T>(this Result<T> result)
    {
        return result.IsSuccess ? 
            Results.Ok(result.Value) 
            : Results.BadRequest(result.Error);    
    }
    
    public static IResult ToApiResponse<T>(this T result)
    {
        return Results.Ok(result);
    }
}