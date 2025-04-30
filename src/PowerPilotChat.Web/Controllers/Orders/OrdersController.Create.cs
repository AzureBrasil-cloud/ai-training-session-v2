using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Web.Extensions;

namespace ContosoAcai.Web.Controllers.Orders;

public partial class OrdersController
{
    [HttpPost("/api/orders")]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrdersRequest request,
        [FromServices] OrderService service)
    {
        return (await service.CreateAsync(request))
            .ToApiResponse();
    }
}