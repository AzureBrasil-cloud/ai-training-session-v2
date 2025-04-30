using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Models.Agent> CreateAgentAsync(
        Credentials credentials, 
        string connectionString, 
        string name,
        string instructions,
        string aiModel)
    {
        var client = CreateAgentsClient(credentials, connectionString);
        
        var agentResponse = await client.CreateAgentAsync(
            model: aiModel,
            name: name,
            instructions: instructions);

        return new Models.Agent(
            agentResponse.Value.Id,
            agentResponse.Value.Name,
            agentResponse.Value.Instructions);
    }
}