using ContosoAcai.Application.Agents.Models.Requests;
using ContosoAcai.Application.Agents.Models.Requests.Validators;
using ContosoAcai.Application.Agents.Models.Results;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application;
using File = ContosoAcai.Infrastructure.AIAgent.Models.File;

namespace ContosoAcai.Application.Agents;

public partial class AgentService
{
    public async Task<Result<MessageResult>> RunAsync(CreateRunRequest request)
    {
        var validationResult = await new CreateRunRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(CreateAsync), error.Message);
            
            return Result<MessageResult>.Failure(error);
        }

        var runResult = await aiAgentService.CreateRunAsync(
            CreateCredentials(),
            request.ThreadId,
            request.Message,
            request.AgentId);

        return Result<MessageResult>.Success(new MessageResult(
            runResult.Role,
            runResult.Content));
    }
}