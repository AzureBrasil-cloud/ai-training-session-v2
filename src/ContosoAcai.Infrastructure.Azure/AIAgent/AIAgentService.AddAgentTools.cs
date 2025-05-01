using Azure.AI.Projects;
using ContosoAcai.Infrastructure.Azure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task AddAgentToolsAsync(Credentials credentials, string agentId, IEnumerable<ITool> tools)
    {
        var client = CreateAgentsClient(credentials);
        
        var toolDefinitions = new List<ToolDefinition>();
        var toolResources = new ToolResources();
        
        (toolDefinitions, toolResources) = AddDocumentTools(
            tools,
            toolDefinitions,
            toolResources);
        
        await client.UpdateAgentAsync(
            agentId,
            tools: toolDefinitions,
            toolResources: toolResources);
    }

    private (List<ToolDefinition> toolDefinitions, ToolResources toolResources) AddDocumentTools(
        IEnumerable<ITool> tools, 
        List<ToolDefinition> toolDefinitions, 
        ToolResources toolResources)
    {
        var documentTools = tools
            .OfType<DocumentTool>()
            .ToList();
        
        if (documentTools.Count == 0)
        {
            return (toolDefinitions, toolResources);
        }

        if (documentTools.Count > 1)
        {
            throw new ArgumentException("Only one document tool is allowed.");
        }

        var  documentTool = documentTools.First();
        
        var fileSearchToolResource = new FileSearchToolResource();
        fileSearchToolResource.VectorStoreIds.Add(documentTool.VectorStoreId);

        toolDefinitions.Add(new FileSearchToolDefinition());
        toolResources.FileSearch = fileSearchToolResource;
        
        return (toolDefinitions, toolResources);
    }
}