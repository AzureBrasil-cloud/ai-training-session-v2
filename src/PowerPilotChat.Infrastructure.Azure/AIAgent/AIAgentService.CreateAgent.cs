using ContosoAcai.Infrastructure.Azure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Agent> CreateAgentAsync(
        Credentials credentials, 
        string name,
        string instructions,
        string aiModel)
    {
        var client = CreateAgentsClient(credentials);
        
        var agentResponse = await client.CreateAgentAsync(
            model: aiModel,
            name: name,
            instructions: instructions);

        return new Agent(
            agentResponse.Value.Id,
            agentResponse.Value.Name,
            agentResponse.Value.Instructions);
    }
}