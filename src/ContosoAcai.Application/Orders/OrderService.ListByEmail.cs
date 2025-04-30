using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Common;
using Microsoft.EntityFrameworkCore;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<IEnumerable<OrderResult>> ListByEmail(ListByEmailQuery query)
    {
        var entities = await context.Orders
            .Where(order => order.UserEmail == query.UserEmail.NormalizeEmail())
            .ToListAsync();
        
        return entities.Select(x => (OrderResult)x);
    }
}