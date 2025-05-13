using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Common;
using Microsoft.EntityFrameworkCore;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<IEnumerable<OrderResult>> ListByEmail(ListByEmailQuery query)
    {
        var queryable =  context.Orders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.UserEmail))
        {
            queryable = queryable.Where(order => order.UserEmail == query.UserEmail.NormalizeEmail());
        }

        var entities = await queryable.ToListAsync();
        
        return entities.Select(x => (OrderResult)x);
    }
}