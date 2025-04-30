using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AiAgentService = ContosoAcai.Infrastructure.Azure.AIAgent.AiAgentService;

namespace ContosoAcai.Infrastructure.Azure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAzure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AiAgentService>();
        return services;
    }
}