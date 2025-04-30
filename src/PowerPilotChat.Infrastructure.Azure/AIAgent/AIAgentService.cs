using Azure.AI.Projects;
using Azure.Identity;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    private AgentsClient CreateAgentsClient(Credentials credentials)
    {
        var credential = new ClientSecretCredential(
            credentials.TenantId, 
            credentials.ClientId, 
            credentials.ClientSecret);
        
        return new AgentsClient(credentials.ProjectConnectionString, credential);
    }
}