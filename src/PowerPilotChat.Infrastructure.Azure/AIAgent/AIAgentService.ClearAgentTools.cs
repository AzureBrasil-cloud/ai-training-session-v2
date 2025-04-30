using Azure.AI.Projects;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task ClearAgentToolsAsync(
        Credentials credentials, 
        string connectionString, 
        string agentId)
    {
        var client = CreateAgentsClient(credentials, connectionString);

        await client.UpdateAgentAsync(
            agentId,
            tools: new List<ToolDefinition>(),
            toolResources: new ToolResources());
    }
}