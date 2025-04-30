using Azure.AI.Projects;
using PowerPilotChat.Infrastructure.Azure.AIAgent.Models;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task AddAgentToolsAsync(
        Credentials credentials, 
        string connectionString, 
        string agentId,
        IEnumerable<ITool> tools)
    {
        var client = CreateAgentsClient(credentials, connectionString);
        
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
        
        FileSearchToolResource fileSearchToolResource = new FileSearchToolResource();
        fileSearchToolResource.VectorStoreIds.Add(documentTool.VectorStoreId);

        toolDefinitions.Add(new FileSearchToolDefinition());
        toolResources.FileSearch = fileSearchToolResource;
        
        return (toolDefinitions, toolResources);
    }
}