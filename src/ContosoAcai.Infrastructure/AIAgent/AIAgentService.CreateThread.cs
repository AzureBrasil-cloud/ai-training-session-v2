using ContosoAcai.Infrastructure.Azure.Shared;
using Thread = ContosoAcai.Infrastructure.AIAgent.Models.Thread;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Thread> CreateThreadAsync(Credentials credentials)
    {
        var client = CreateAgentsClient(credentials);

        var threadResponse = await client.Threads.CreateThreadAsync();

        return new Thread(threadResponse.Value.Id);
    }
}