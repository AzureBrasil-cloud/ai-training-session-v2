using ContosoAcai.Application.Orders.Models.Requests;
using ContosoAcai.Application.Orders.Models.Requests.Validators;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Data.Entities;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    public async Task<Result<AudioPreOrderResult>> CreateAsync(CreateAudioPreOrdersRequest request)
    {
        var validationResult = await new CreateAudioPreOrdersRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(CreateAsync), error.Message);
            
            return Result<AudioPreOrderResult>.Failure(error);
        }
        
        var content = await speechService.TranscribeAsync(
            new ApiKeyCredentials(
                configuration["AI:Speech:Endpoint"]!,
                configuration["AI:Speech:Key"]!),
            request.Content,
            request.AudioName);

        var audioPreOder = new AudioPreOrder(
            request.UserEmail,
            request.AudioName,
            request.AudioExtension,
            content.CombinedPhrases.FirstOrDefault()?.Text!);
        
        await context.AudioPreOrders.AddAsync(audioPreOder);
        await context.SaveChangesAsync();
        
        return Result<AudioPreOrderResult>.Success((AudioPreOrderResult)audioPreOder);
    }
}