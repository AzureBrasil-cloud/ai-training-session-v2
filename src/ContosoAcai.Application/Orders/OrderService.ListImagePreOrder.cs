using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Data.Entities;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.EntityFrameworkCore;
using PowerPilotChat.Application;

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