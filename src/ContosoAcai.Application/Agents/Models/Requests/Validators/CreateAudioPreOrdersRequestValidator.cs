using FluentValidation;

namespace ContosoAcai.Application.Agents.Models.Requests.Validators;

public class CreateRunRequestValidator : AbstractValidator<CreateRunRequest>
{
    public CreateRunRequestValidator()
    {
        RuleFor(x => x.AgentId)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.AgentId)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.AgentId)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.AgentId)).Args);

        RuleFor(x => x.ThreadId)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.ThreadId)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.ThreadId)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.ThreadId)).Args);

        RuleFor(x => x.Message)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.Message)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.Message)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateRunRequest.Message)).Args);
    }
}