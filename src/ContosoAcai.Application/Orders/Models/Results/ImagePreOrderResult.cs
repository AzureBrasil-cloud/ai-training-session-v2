namespace ContosoAcai.Application.Orders.Models.Results;

public record ImagePreOrderResult(
    Guid Id,
    string UserEmail,
    string Name,
    string Extension,
    List<string> KeyValuePairs,
    DateTime CreatedAt);