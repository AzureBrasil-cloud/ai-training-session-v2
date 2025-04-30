using Azure.AI.Projects;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Models.File> UploadFile(
        Credentials credentials, 
        string connectionString,
        Stream content,
        string fileName)
    {
        var client = CreateAgentsClient(credentials, connectionString);

        var fileResponse = await client.UploadFileAsync(content, AgentFilePurpose.Agents, fileName);

        return new Models.File(fileResponse.Value.Id);
    }
}