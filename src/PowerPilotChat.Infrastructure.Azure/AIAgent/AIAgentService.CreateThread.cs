using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Models.Thread> CreateThreadAsync(Credentials credentials, string connectionString)
    {
        var client = CreateAgentsClient(credentials, connectionString);

        var threadResponse = await client.CreateThreadAsync();

        return new Models.Thread(threadResponse.Value.Id);
    }
}