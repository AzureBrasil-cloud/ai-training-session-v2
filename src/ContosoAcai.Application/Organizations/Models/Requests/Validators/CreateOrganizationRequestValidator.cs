using FluentValidation;
using PowerPilotChat.Common;

namespace PowerPilotChat.Application.Organizations.Models.Requests.Validators;

public class CreateOrganizationRequestValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationRequestValidator()
    {
        RuleFor(x => x.TenantId)
            .Must(x => x != Guid.Empty)
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.TenantId)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.TenantId)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.TenantId)).Args);
        
        RuleFor(x => x.LegalName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.LegalName)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.LegalName)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.LegalName)).Args);
        
        RuleFor(x => x.FantasyName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.FantasyName)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.FantasyName)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.FantasyName)).Args);
        
        RuleFor(x => x.PersonalIdentifier)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithErrorCode(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.PersonalIdentifier)).Code.ToString())
            .WithMessage(Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.PersonalIdentifier)).RawMessage)
            .WithState(x => Errors.NotBeNullOrEmpty(nameof(CreateOrganizationRequest.PersonalIdentifier)).Args);
        
        RuleFor(x => x.PersonalIdentifier)
            .Must(StringUtils.IsValidPersonalIdentifier)
            .WithErrorCode(Errors.InvalidPersonalIdentifier().Code.ToString())
            .WithMessage(Errors.InvalidPersonalIdentifier().RawMessage)
            .WithState(x => Errors.InvalidPersonalIdentifier().Args);
    }
}