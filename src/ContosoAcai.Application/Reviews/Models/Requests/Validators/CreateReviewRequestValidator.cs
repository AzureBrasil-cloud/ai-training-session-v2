using FluentValidation;

namespace ContosoAcai.Application.Reviews.Models.Requests.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.UserEmail)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.UserEmail)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.UserEmail)).Args);

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.Content)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.Content)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateReviewRequest.Content)).Args);
    }
}