using ContosoAcai.Application.Orders.Models.Results;
using Microsoft.EntityFrameworkCore;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<IList<ImagePreOrderResult>> ListImagePreOrdersAsync()
    {
        var imagePreOrders = await context.ImagePreOrders
            .ToListAsync();

        return imagePreOrders.Select(x => (ImagePreOrderResult)x).ToList();
    }
}