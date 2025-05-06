using Azure.AI.Projects;
using ContosoAcai.Infrastructure.Azure.Shared;
using File = ContosoAcai.Infrastructure.AIAgent.Models.File;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<File> UploadFileAsync(
        Credentials credentials, 
        Stream content,
        string fileName)
    {
        var client = CreateAgentsClient(credentials);

        var fileResponse = await client.UploadFileAsync(content, AgentFilePurpose.Agents, fileName);

        return new File(fileResponse.Value.Id);
    }
}