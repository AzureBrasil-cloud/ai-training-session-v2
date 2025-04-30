using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task UpdateVectorStoreAsync(
        Credentials credentials,
        string connectionString,
        string vectorStoreId,
        string name)
    {
        var client = CreateAgentsClient(credentials, connectionString);
        await client.ModifyVectorStoreAsync(vectorStoreId, name);
    }
}