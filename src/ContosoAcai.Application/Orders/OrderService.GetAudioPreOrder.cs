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
                                        Extract the order information from the transcription. Respond **only** with a JSON object matching the provided schema. Requirements:
                                        - Property "size": a string equal to one of:
                                            - "1" for Small
                                            - "2" for Medium
                                            - "3" for Large
                                        - Map Portuguese terms if needed: pequeno→Small, médio→Medium, grande→Large.
                                        - Property "extras": an array of strings listing each extra.
                                        - Do not include any other properties, labels or explanatory text.

                                        Example output:
                                        {
                                          "size": "1",
                                          "extras": ["M&Ms", "Chocolate"]
                                        }
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