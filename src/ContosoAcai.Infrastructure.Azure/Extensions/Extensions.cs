using ContosoAcai.Infrastructure.Azure.AIAgent;
using ContosoAcai.Infrastructure.Azure.AiInference;
using ContosoAcai.Infrastructure.Azure.DocumentIntelligence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoAcai.Infrastructure.Azure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAzure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AiAgentService>();
        services.AddScoped<DocumentIntelligenceService>();
        services.AddScoped<AiInferenceService>();
        return services;
    }
}