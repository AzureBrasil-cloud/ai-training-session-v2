using ContosoAcai.Data;
using ContosoAcai.Data.Entities;
using ContosoAcai.Application.Reviews.Models.Requests;
using ContosoAcai.Application.Reviews.Models.Requests.Validators;
using ContosoAcai.Application.Reviews.Models.Results;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.Extensions.Logging;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Reviews;

public partial class ReviewService
{
    private const string Instructions = """
                                        You are a review classifier bot, an expert at judging customer satisfaction with açaí orders.
                                        Your task is to map each review to the corresponding integer in this enum:
                                        
                                        1 – VeryBad  
                                        2 – Bad  
                                        3 – Neutral  
                                        4 – Good  
                                        5 – VeryGood  
                                        6 – Unknown  (use when the text is unrelated or sentiment is unclear)
                                        
                                        Rules
                                        • Output only the integer (1‑6).  
                                        • No extra text, punctuation, or leading zeros.  
                                        • If multiple sentiments appear, choose the predominant one.  
                                        • If you are unsure, return 6.
                                        
                                        Few‑shot examples
                                        
                                        User: Recebi meu açaí todo derretido e sem granola, péssimo!
                                        Assistant: 1
                                        
                                        User: O açaí veio azedo, parecia estragado.
                                        Assistant: 1
                                        
                                        User: Tinha cristais de gelo enormes, impossível comer.
                                        Assistant: 1
                                        
                                        User: Açaí muito ralo, quase sem sabor.
                                        Assistant: 2
                                        
                                        User: Parecia artificial e enjoativo, não gostei.
                                        Assistant: 2
                                        
                                        User: Textura estava ok, mas o sabor poderia ser melhor.
                                        Assistant: 3
                                        
                                        User: Nada de especial, apenas mediano.
                                        Assistant: 3
                                        
                                        User: Açaí cremoso e sabor equilibrado, gostei.
                                        Assistant: 4
                                        
                                        User: Boa consistência, veio bem gelado, muito bom!
                                        Assistant: 4
                                        
                                        User: Sabor incrível, textura perfeita!
                                        Assistant: 5
                                        
                                        User: Melhor açaí que já experimentei, super cremoso.
                                        Assistant: 5
                                        
                                        User: Gostou do jogo ontem?
                                        Assistant: 6
                                        """;
    
    public async Task<Result<ReviewResult>> CreateAsync(CreateReviewRequest request)
    {
        var validationResult = await new CreateReviewRequestValidator().ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = ErrorHandler.CreateErrorFromValidationResult(validationResult);
            logger.LogInformation("{Method} - {Message}", nameof(CreateAsync), error.Message);

            return Result<ReviewResult>.Failure(error);
        }

        var inferenceResult = await aiInferenceService.CompleteAsync(
            new ApiKeyCredentials(
                configuration["AI:Inference:Endpoint"]!,
                configuration["AI:Inference:Key"]!),
            configuration["AI:Inference:Model"]!,
            Instructions,
            request.Content);
        
        var review = new Review(request.UserEmail, request.Content, ToReviewClassification(inferenceResult));

        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();

        return Result<ReviewResult>.Success((ReviewResult)review);
    }
    
    private static ReviewClassification ToReviewClassification(string numericText)
    {
        if (int.TryParse(numericText, out var numericValue) &&
            Enum.IsDefined(typeof(ReviewClassification), numericValue))
        {
            return (ReviewClassification)numericValue;
        }

        return ReviewClassification.Unknown;
    }
}