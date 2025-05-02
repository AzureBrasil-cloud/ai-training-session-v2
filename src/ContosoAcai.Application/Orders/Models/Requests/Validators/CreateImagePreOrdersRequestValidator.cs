using ContosoAcai.Common;
using FluentValidation;

namespace ContosoAcai.Application.Orders.Models.Requests.Validators;

public class CreateImagePreOrdersRequestValidator : AbstractValidator<CreateImagePreOrdersRequest>
{
    public CreateImagePreOrdersRequestValidator()
    {
        RuleFor(x => x.UserEmail)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Args);
        
        RuleFor(x => x.Name)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Name)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Name)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Name)).Args);

        RuleFor(x => x.Extension)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Extension)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Extension)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Extension)).Args);
        
        RuleFor(x => x.Extension)
            .Must(x => x.IsValidFileExtension())
            .WithErrorCode(Errors.InvalidFileExtension().Code.ToString())
            .WithMessage(Errors.InvalidFileExtension().RawMessage)
            .WithState(x => Errors.InvalidFileExtension().Args);
            
        RuleFor(x => x.Content)
            .NotNull()
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Content)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Content)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateImagePreOrdersRequest.Content)).Args);
    }
}