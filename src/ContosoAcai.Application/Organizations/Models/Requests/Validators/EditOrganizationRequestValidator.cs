using FluentValidation;
using PowerPilotChat.Common;

namespace PowerPilotChat.Application.Organizations.Models.Requests.Validators;

public class EditOrganizationRequestValidator : AbstractValidator<EditOrganizationRequest>
{
    public EditOrganizationRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty)
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.Id)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.Id)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.Id)).Args);
        
        RuleFor(x => x.LegalName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.LegalName)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.LegalName)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.LegalName)).Args);
        
        RuleFor(x => x.FantasyName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.FantasyName)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.FantasyName)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.FantasyName)).Args);
        
        RuleFor(x => x.PersonalIdentifier)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.PersonalIdentifier)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.PersonalIdentifier)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(EditOrganizationRequest.PersonalIdentifier)).Args);
        
        RuleFor(x => x.PersonalIdentifier)
            .Must(StringUtils.IsValidPersonalIdentifier)
            .WithErrorCode(Errors.InvalidPersonalIdentifier().Code.ToString())
            .WithMessage(Errors.InvalidPersonalIdentifier().RawMessage)
            .WithState(x => Errors.InvalidPersonalIdentifier().Args);
    }
}