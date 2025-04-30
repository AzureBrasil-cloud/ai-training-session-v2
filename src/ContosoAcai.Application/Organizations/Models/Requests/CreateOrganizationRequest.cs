namespace PowerPilotChat.Application.Organizations.Models.Requests;

public record CreateOrganizationRequest(
    Guid TenantId,
    string LegalName,
    string FantasyName,
    string PersonalIdentifier);