using Azure.AI.Projects;
using ContosoAcai.Infrastructure.Azure.Shared;
using VectorStore = ContosoAcai.Infrastructure.Azure.AIAgent.Models.VectorStore;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<VectorStore> CreateVectorStoreAsync(
        Credentials credentials, 
        string name,
        IEnumerable<string> files)
    {
        var client = CreateAgentsClient(credentials);

        var vectorStore = await client.CreateVectorStoreAsync(
            fileIds:  files,
            name: name,
            expiresAfter: new VectorStoreExpirationPolicy(VectorStoreExpirationPolicyAnchor.LastActiveAt, 2));

        return new VectorStore(vectorStore.Value.Id, vectorStore.Value.Name);
    }
}