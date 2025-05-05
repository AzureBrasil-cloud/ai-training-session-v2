using ContosoAcai.Application.Threads;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoAcai.Web.Controllers.Threads;

public partial class ThreadsController
{
    [HttpPost("/api/threads")]
    public async Task<IActionResult> Create([FromServices] ThreadService service)
    {
        return (await service.CreateAsync()).ToApiResponse();
    }
}