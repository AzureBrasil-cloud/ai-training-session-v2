using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task RemoveFilesFromVectorStoreAsync(
        Credentials credentials, 
        string vectorStoreId, 
        IEnumerable<string> fileIds)
    {
        var client = CreateAgentsClient(credentials);

        foreach (var fileId in fileIds)
        {
            await client.DeleteVectorStoreFileAsync(vectorStoreId, fileId);
        }
    }
}