using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application.Organizations.Models.Requests;
using PowerPilotChat.Application.Organizations.Models.Requests.Validators;
using PowerPilotChat.Application.Organizations.Models.Results;
using PowerPilotChat.Common;
using PowerPilotChat.Data.Entities;

namespace PowerPilotChat.Application.Organizations;

public partial class OrganizationService
{
    public async Task<Result<OrganizationResult>> EditOrganizationAsync(EditOrganizationRequest request)
    {
        var validationResult = await new EditOrganizationRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(EditOrganizationAsync), error.Message);
            
            return Result<OrganizationResult>.Failure(error);
        }
        
        var organization = await context.Organizations.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (organization == null)
        {
            var error = Errors.EntityNotFound(nameof(Organization), request.Id.ToString());
            logger.LogInformation("{Method} - {Message}", nameof(EditOrganizationAsync), error.Message);
            return Result<OrganizationResult>.Failure(error);
        }
        
        organization.Update(
            request.FantasyName,
            request.LegalName,
            request.PersonalIdentifier);
        
        await context.SaveChangesAsync();
        
        return Result<OrganizationResult>.Success((OrganizationResult)organization);
    }
}