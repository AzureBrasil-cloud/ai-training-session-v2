using ContosoAcai.Application.Agents;
using ContosoAcai.Application.Agents.Models.Requests;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoAcai.Web.Controllers.Agents;

public partial class AgentsController
{
    [HttpPost("/api/agents/run")]
    public async Task<IActionResult> Run(
        [FromBody] CreateRunRequest request,
        [FromServices] AgentService service)
    {
        return (await service.RunAsync(request)).ToApiResponse();
    }
}