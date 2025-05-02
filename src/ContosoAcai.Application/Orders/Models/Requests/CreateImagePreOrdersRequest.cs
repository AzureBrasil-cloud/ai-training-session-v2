namespace ContosoAcai.Application.Orders.Models.Requests;

public record CreateImagePreOrdersRequest(
    string UserEmail,
    string Name,
    string Extension,
    Stream Content);