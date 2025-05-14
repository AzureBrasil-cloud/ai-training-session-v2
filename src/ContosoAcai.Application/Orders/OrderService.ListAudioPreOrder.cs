using ContosoAcai.Application.Orders.Models.Results;
using Microsoft.EntityFrameworkCore;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<IList<AudioPreOrderResult>> ListAudioPreOrder()
    {
        var imagePreOrders = await context.AudioPreOrders
            .ToListAsync();

        return imagePreOrders.Select(x => (AudioPreOrderResult)x).ToList();
    }
}