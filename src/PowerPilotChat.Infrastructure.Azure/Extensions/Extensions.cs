using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerPilotChat.Infrastructure.Azure.AIAgent;
using PowerPilotChat.Infrastructure.Azure.ResourceManager;

namespace PowerPilotChat.Infrastructure.Azure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAzure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ResourceManagerService>();
        services.AddScoped<AiAgentService>();
        
        return services;
    }
}