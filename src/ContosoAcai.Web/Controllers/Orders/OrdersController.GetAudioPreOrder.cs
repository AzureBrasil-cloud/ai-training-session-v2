using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Web.Extensions;

namespace ContosoAcai.Web.Controllers.Orders;

public partial class OrdersController
{
    [HttpGet("/api/pre-order/audio/{id}")]
    public async Task<IActionResult> GetAudioPreOrder(
        Guid id,
        bool applyAiTransformation,
        [FromServices] OrderService service)
    {
        return (await service.GetByIdAsync(new GetAudioPreOrderQuery(id, applyAiTransformation))).ToApiResponse();
    }
}