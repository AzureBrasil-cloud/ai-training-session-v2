using Azure.AI.Projects;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Models.VectorStore> CreateVectorStoreAsync(
        Credentials credentials, 
        string connectionString,
        string name,
        IEnumerable<string> files)
    {
        var client = CreateAgentsClient(credentials, connectionString);

        var vectorStore = await client.CreateVectorStoreAsync(
            fileIds:  files,
            name: name,
            //todo: implement expiration policy
            expiresAfter: new VectorStoreExpirationPolicy(VectorStoreExpirationPolicyAnchor.LastActiveAt, 999));

        return new Models.VectorStore(vectorStore.Value.Id, vectorStore.Value.Name);
    }
}