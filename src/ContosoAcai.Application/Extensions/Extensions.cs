using ContosoAcai.Infrastructure.Azure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerPilotChat.Application.Organizations;
using PowerPilotChat.Data.Extensions;

namespace PowerPilotChat.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<OrganizationService>();
        
        // Domain
        services.AddData(configuration);
        
        // Infra
        services.AddAzure(configuration);
        
        return services;
    }
}