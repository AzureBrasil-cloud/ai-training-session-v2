using PowerPilotChat.Data.Entities;

namespace PowerPilotChat.Application.Organizations.Models.Results;

public record OrganizationResult(
    Guid Id,
    Guid TenantId, 
    string LegalName,
    string FantasyName,
    string PersonalIdentifier)
{
    public static implicit operator OrganizationResult(Organization organization)
    {
        return new OrganizationResult(
            organization.Id, 
            organization.TenantId, 
            organization.LegalName,
            organization.FantasyName,
            organization.PersonalIdentifier);
    }
}