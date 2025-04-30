using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application.Organizations.Models.Requests;
using PowerPilotChat.Application.Organizations.Models.Requests.Validators;
using PowerPilotChat.Application.Organizations.Models.Results;
using PowerPilotChat.Data.Entities;

namespace PowerPilotChat.Application.Organizations;

public partial class OrganizationService
{
    public async Task<Result<OrganizationResult>> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        var validationResult = await new CreateOrganizationRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(CreateOrganizationAsync), error.Message);
            
            return Result<OrganizationResult>.Failure(error);
        }
        
        var exists = await context.Organizations.AnyAsync(x => x.TenantId == request.TenantId);

        if (exists)
        {
            var error = Errors.EntityExists(nameof(Organization), request.TenantId.ToString());
            logger.LogInformation("{Method} - {Message}", nameof(CreateOrganizationAsync), error.Message);
            
            return Result<OrganizationResult>.Failure(error);
        }
        
        var organization = new Organization(
            request.TenantId,
            request.FantasyName,
            request.LegalName,
            request.PersonalIdentifier);
        
        await context.Organizations.AddAsync(organization);
        await context.SaveChangesAsync();

        return Result<OrganizationResult>.Success((OrganizationResult)organization);
    }
}