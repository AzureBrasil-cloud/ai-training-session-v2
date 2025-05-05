using ContosoAcai.Application.Threads;
using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Web.Extensions;

namespace ContosoAcai.Web.Controllers.Threads;

public partial class ThreadsController
{
    [HttpPost("/api/threads")]
    public async Task<IActionResult> Create([FromServices] ThreadService service)
    {
        return (await service.CreateAsync()).ToApiResponse();
    }
}