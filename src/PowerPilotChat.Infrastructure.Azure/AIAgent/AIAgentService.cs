using Azure.AI.Projects;
using Azure.Identity;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    private AgentsClient CreateAgentsClient(Credentials credentials, string connectionString)
    {
        var credential = new ClientSecretCredential(
            credentials.TenantId, 
            credentials.ClientId, 
            credentials.ClientSecret);
        
        return new AgentsClient(connectionString, credential);
    }
}