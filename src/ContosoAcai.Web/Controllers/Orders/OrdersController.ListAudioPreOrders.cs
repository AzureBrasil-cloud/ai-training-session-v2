using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Requests;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoAcai.Web.Controllers.Orders;

public partial class OrdersController
{
    [HttpGet("/api/pre-order/audio")]
    public async Task<IActionResult> ListAudioPreOrder([FromServices] OrderService service)
    {
        return (await service.ListAudioPreOrder()).ToApiResponse();
    }
}