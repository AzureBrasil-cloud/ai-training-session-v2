using ContosoAcai.Data.Entities;

namespace ContosoAcai.Application.Orders.Models.Requests;

public record CreateOrdersRequest(
    string UserEmail,
    Size Size,
    string[] Extras);