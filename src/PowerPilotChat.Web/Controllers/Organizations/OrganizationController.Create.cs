using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Application.Organizations;
using PowerPilotChat.Application.Organizations.Models.Requests;
using PowerPilotChat.Web.Extensions;

namespace PowerPilotChat.Web.Controllers.Organizations;

public partial class OrganizationController
{
    [HttpPost("/api/organizations")]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrganizationRequest request,
        [FromServices] OrganizationService organizationService)
    {
        // return (await organizationService.CreateOrganizationAsync(request with { TenantId =  UserTenantId })).ToApiResponse();

        return Ok();
    }
}