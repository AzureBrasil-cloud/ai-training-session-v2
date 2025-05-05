using ContosoAcai.Data;
using ContosoAcai.Infrastructure.Azure.AIAgent;
using Microsoft.Extensions.Configuration;

namespace ContosoAcai.Application.Threads;

public partial class ThreadService(
    IConfiguration configuration, 
    AppDbContext context,
    AiAgentService aiAgentService);
