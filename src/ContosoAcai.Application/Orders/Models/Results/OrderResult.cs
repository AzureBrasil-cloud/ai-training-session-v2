using ContosoAcai.Data.Entities;

namespace ContosoAcai.Application.Orders.Models.Results;

public record OrderResult(
    Guid Id,
    string UserEmail,
    Size Size,
    string[] Extras,
    double TotalValue,
    DateTime CreatedAt)
{
    public static implicit operator OrderResult(Order order)
    {
        return new OrderResult(
            order.Id,
            order.UserEmail,
            order.Size,
            order.Extras,
            order.TotalValue,
            order.CreatedAt);
    }
}