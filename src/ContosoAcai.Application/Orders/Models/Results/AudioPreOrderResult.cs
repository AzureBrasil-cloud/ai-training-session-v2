using ContosoAcai.Data.Entities;

namespace ContosoAcai.Application.Orders.Models.Results;

public record AudioPreOrderResult(
    Guid Id,
    string UserEmail,
    string AudioName,
    string AudioExtension,
    string Content,
    DateTime CreatedAt)
{
    public AiTransformedOrderResult? AiTransformedOrder  { get; set; }
    
    public static implicit operator AudioPreOrderResult(AudioPreOrder order)
    {
        return new AudioPreOrderResult(
            order.Id,
            order.UserEmail,
            order.AudioName,
            order.AudioExtension,
            order.Content,
            order.CreatedAt);
    }
}