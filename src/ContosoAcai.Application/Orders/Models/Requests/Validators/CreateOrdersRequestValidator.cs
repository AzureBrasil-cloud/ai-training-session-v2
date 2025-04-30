using FluentValidation;

namespace ContosoAcai.Application.Orders.Models.Requests.Validators;

public class CreateOrdersRequestValidator : AbstractValidator<CreateOrdersRequest>
{
    public CreateOrdersRequestValidator()
    {
        RuleFor(x => x.UserEmail)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrdersRequest.UserEmail)).Args);

        RuleFor(x => x.Size)
            .IsInEnum()
            .WithErrorCode(Errors.InvalidEnum(nameof(CreateOrdersRequest.Size)).Code.ToString())
            .WithMessage(Errors.InvalidEnum(nameof(CreateOrdersRequest.Size)).RawMessage)
            .WithState(x => Errors.InvalidEnum(nameof(CreateOrdersRequest.Size)).Args);
    }
}