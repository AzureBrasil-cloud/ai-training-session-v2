using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Data.Entities;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.EntityFrameworkCore;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    private string Instructions => """
                                   Extract the order information;
                                   """;
    
    public async Task<ImagePreOrderResult?> GetByIdAsync(GetImagePreOrderQuery query)
    {
        var imagePreOrder = await context.ImagePreOrders
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        if (imagePreOrder is null)
        {
            return null;
        }
        
        var result = (ImagePreOrderResult)imagePreOrder;

        if (!query.ApplyAiTransformation) return result;
        
        var inferenceResult = await aiInferenceService.CompleteAsync<AiTransformedOrder>(
            new ApiKeyCredentials(
                configuration["AI:Inference:Endpoint"]!,
                configuration["AI:Inference:Key"]!),
            configuration["AI:Inference:Model"]!,
            Instructions,
            string.Join("\n", imagePreOrder.KeyValuePairs));
            
        result.AiTransformedOrder = inferenceResult;

        return result;
    }
}