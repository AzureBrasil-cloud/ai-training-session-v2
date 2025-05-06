using ContosoAcai.Data;
using Microsoft.Extensions.Configuration;
using AiAgentService = ContosoAcai.Infrastructure.AIAgent.AiAgentService;

namespace ContosoAcai.Application.Threads;

public partial class ThreadService(
    IConfiguration configuration, 
    AppDbContext context,
    AiAgentService aiAgentService);
