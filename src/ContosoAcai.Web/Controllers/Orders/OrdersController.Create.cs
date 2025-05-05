using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Requests;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

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