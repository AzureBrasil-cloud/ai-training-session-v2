using ContosoAcai.Common;
using FluentValidation;

namespace ContosoAcai.Application.Orders.Models.Requests.Validators;

public class CreateAudioPreOrdersRequestValidator : AbstractValidator<CreateAudioPreOrdersRequest>
{
    public CreateAudioPreOrdersRequestValidator()
    {
        RuleFor(x => x.UserEmail)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Args);
        
        RuleFor(x => x.AudioName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioName)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioName)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioName)).Args);

        RuleFor(x => x.AudioExtension)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioExtension)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioExtension)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.AudioExtension)).Args);
        
        RuleFor(x => x.AudioExtension)
            .Must(x => x.IsValidAudioExtension())
            .WithErrorCode(Errors.InvalidFileExtension().Code.ToString())
            .WithMessage(Errors.InvalidFileExtension().RawMessage)
            .WithState(x => Errors.InvalidFileExtension().Args);
            
        RuleFor(x => x.Content)
            .NotNull()
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.Content)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.Content)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateAudioPreOrdersRequest.Content)).Args);
    }
}