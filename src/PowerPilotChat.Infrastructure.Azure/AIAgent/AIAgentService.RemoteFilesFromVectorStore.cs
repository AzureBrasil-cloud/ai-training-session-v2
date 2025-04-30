using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task RemoveFilesFromVectorStoreAsync(
        Credentials credentials, 
        string connectionString, 
        string vectorStoreId, 
        IEnumerable<string> fileIds)
    {
        var client = CreateAgentsClient(credentials, connectionString);

        foreach (var fileId in fileIds)
        {
            await client.DeleteVectorStoreFileAsync(vectorStoreId, fileId);
        }
    }
}