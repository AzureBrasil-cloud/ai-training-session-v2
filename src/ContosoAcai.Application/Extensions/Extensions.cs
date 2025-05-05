using ContosoAcai.Application.Agents;
using ContosoAcai.Application.Orders;
using ContosoAcai.Data.Extensions;
using ContosoAcai.Infrastructure.Azure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoAcai.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<OrderService>();
        services.AddScoped<AgentService>();
        
        // Domain
        services.AddData(configuration);
        
        // Infra
        services.AddAzure(configuration);
        
        return services;
    }
}