using Azure.AI.Agents.Persistent;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task ClearAgentToolsAsync(Credentials credentials, string agentId)
    {
        var client = CreateAgentsClient(credentials);

        await client.Administration.UpdateAgentAsync(
            agentId,
            tools: new List<ToolDefinition>(),
            toolResources: new ToolResources());
    }
}