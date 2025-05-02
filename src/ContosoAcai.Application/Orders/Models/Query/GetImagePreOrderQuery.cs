namespace ContosoAcai.Application.Orders.Models.Query;

public record GetImagePreOrderQuery(Guid Id, bool ApplyAiTransformation = false);