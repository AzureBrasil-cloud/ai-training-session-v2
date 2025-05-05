using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Requests;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoAcai.Web.Controllers.Orders;

public partial class OrdersController
{
    [HttpPost("/api/pre-order/image")]
    public async Task<IActionResult> CreateImagePreOrder(
        [FromForm] IFormFile file,
        [FromForm] string userEmail,
        [FromServices] OrderService service)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        await using var stream = file.OpenReadStream();
        
        return (await service.CreateAsync(new CreateImagePreOrdersRequest(
                userEmail,
                fileNameWithoutExtension,
                extension,
                stream)))
            .ToApiResponse();
    }
}