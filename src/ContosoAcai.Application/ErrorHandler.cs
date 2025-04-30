using FluentValidation.Results;

namespace PowerPilotChat.Application;

public static class ErrorHandler
{
    public static Error CreateErrorFromValidationResult(ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(x => new Error(Guid.Parse(x.ErrorCode), x.ErrorMessage, (Dictionary<string, object>)x.CustomState))
            .First();
    }
}