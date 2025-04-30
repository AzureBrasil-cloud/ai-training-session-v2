using ContosoAcai.Infrastructure.Azure.Shared;
using Thread = ContosoAcai.Infrastructure.Azure.AIAgent.Models.Thread;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Thread> CreateThreadAsync(Credentials credentials)
    {
        var client = CreateAgentsClient(credentials);

        var threadResponse = await client.CreateThreadAsync();

        return new Thread(threadResponse.Value.Id);
    }
}