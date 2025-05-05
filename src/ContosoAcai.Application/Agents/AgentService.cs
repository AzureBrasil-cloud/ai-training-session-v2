using ContosoAcai.Data;
using ContosoAcai.Infrastructure.Azure.AIAgent;
using ContosoAcai.Infrastructure.Azure.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Application.Agents;

public partial class AgentService(
    AppDbContext context,
    IConfiguration configuration,
    ILogger<AgentService> logger,
    AiAgentService aiAgentService)
{
    private Credentials CreateCredentials()
    {
        return new Credentials(
            configuration["AI:Agent:TenantId"]!,
            configuration["AI:Agent:ClientId"]!,
            configuration["AI:Agent:ClientSecret"]!,
            configuration["AI:Agent:ConnectionString"]!);
    }
};
