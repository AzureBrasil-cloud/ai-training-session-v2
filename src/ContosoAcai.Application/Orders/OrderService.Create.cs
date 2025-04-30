using ContosoAcai.Application.Orders.Models.Requests;
using ContosoAcai.Application.Orders.Models.Requests.Validators;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Data.Entities;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<Result<OrderResult>> CreateAsync(CreateOrdersRequest request)
    {
        var validationResult = await new CreateOrdersRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(CreateAsync), error.Message);
            
            return Result<OrderResult>.Failure(error);
        }

        var order = new Order(
            request.UserEmail,
            request.Size,
            request.Extras);
        
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Result<OrderResult>.Success((OrderResult)order);
    }
}