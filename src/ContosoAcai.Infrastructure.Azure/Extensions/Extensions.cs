using ContosoAcai.Infrastructure.Azure.AiInference;
using ContosoAcai.Infrastructure.Azure.DocumentIntelligence;
using ContosoAcai.Infrastructure.Azure.Speech;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AiAgentService = ContosoAcai.Infrastructure.AIAgent.AiAgentService;

namespace ContosoAcai.Infrastructure.Azure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAzure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        
        services.AddScoped<AiAgentService>();
        services.AddScoped<DocumentIntelligenceService>();
        services.AddScoped<AiInferenceService>();
        services.AddScoped<SpeechService>();
 
        return services;
    }
}