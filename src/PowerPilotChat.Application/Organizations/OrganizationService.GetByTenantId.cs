using Microsoft.EntityFrameworkCore;
using PowerPilotChat.Application.Organizations.Models.Results;
using PowerPilotChat.Data.Entities;

namespace PowerPilotChat.Application.Organizations;

public partial class OrganizationService
{
    public async Task<Result<OrganizationResult>> GetByTenantIdAsync(Guid tenantId)
    {
        var result = await context.Organizations.FirstOrDefaultAsync(x => x.TenantId == tenantId);

        if (result is null)
            return Result<OrganizationResult>.Failure(Errors.EntityNotFound(nameof(Organization), tenantId.ToString()));

        return (OrganizationResult)result;
    }
}