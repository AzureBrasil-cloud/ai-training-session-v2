using ContosoAcai.Data.Entities;

namespace ContosoAcai.Application.Orders.Models.Results;

public record ImagePreOrderResult(
    Guid Id,
    string UserEmail,
    string ImageName,
    string ImageExtension,
    string[] KeyValuePairs,
    DateTime CreatedAt)
{
    public static implicit operator ImagePreOrderResult(ImagePreOrder order)
    {
        return new ImagePreOrderResult(
            order.Id,
            order.UserEmail,
            order.ImageName,
            order.ImageExtension,
            order.KeyValuePairs,
            order.CreatedAt);
    }
}