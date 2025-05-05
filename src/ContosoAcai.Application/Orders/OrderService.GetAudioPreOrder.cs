using ContosoAcai.Application.Orders.Models.Query;
using ContosoAcai.Application.Orders.Models.Results;
using ContosoAcai.Data.Entities;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.EntityFrameworkCore;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Orders;

public partial class OrderService
{
    private string InstructionsAudio => """
                                   Extract the order information;
                                   """;
    
    public async Task<AudioPreOrderResult?> GetByIdAsync(GetAudioPreOrderQuery query)
    {
        var audioPreOrder = await context.AudioPreOrders
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        if (audioPreOrder is null)
        {
            return null;
        }
        
        var result = (AudioPreOrderResult)audioPreOrder;

        if (!query.ApplyAiTransformation) return result;
        
        var inferenceResult = await aiInferenceService.CompleteAsync<AiTransformedOrder>(
            new ApiKeyCredentials(
                configuration["AI:Inference:Endpoint"]!,
                configuration["AI:Inference:Key"]!),
            configuration["AI:Inference:Model"]!,
            InstructionsAudio,
            audioPreOrder.Content);
            
        result.AiTransformedOrder = inferenceResult;

        return result;
    }
}