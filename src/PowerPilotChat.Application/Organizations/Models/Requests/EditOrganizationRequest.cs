namespace PowerPilotChat.Application.Organizations.Models.Requests;

public record EditOrganizationRequest(
    Guid Id,
    string LegalName,
    string FantasyName,
    string PersonalIdentifier);