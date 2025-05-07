using ContosoAcai.Application.Agents;
using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Reviews;
using ContosoAcai.Application.Threads;
using ContosoAcai.Data.Extensions;
using ContosoAcai.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoAcai.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<ReviewService>();
        services.AddScoped<OrderService>();
        services.AddScoped<ThreadService>();
        services.AddScoped<AgentService>();
        
        // Domain
        services.AddData(configuration);
        
        // Infra
        services.AddAzure(configuration);
        
        return services;
    }
}