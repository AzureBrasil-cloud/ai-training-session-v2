using ContosoAcai.Data;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Application.Orders;

public partial class OrderService(
    AppDbContext context, 
    ILogger<OrderService> logger);
