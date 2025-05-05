using ContosoAcai.Data;
using ContosoAcai.Infrastructure.Azure.AIAgent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Application.Agents;

public partial class AgentService(
    AppDbContext context,
    IConfiguration configuration,
    ILogger<AgentService> logger,
    AiAgentService aiAgentService);
