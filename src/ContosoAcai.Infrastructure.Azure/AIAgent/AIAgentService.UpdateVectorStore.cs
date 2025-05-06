using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task UpdateVectorStoreAsync(
        Credentials credentials,
        string vectorStoreId,
        string name)
    {
        var client = CreateAgentsClient(credentials);
        await client.ModifyVectorStoreAsync(vectorStoreId, name);
    }
}