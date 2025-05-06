using System.Text.Json;
using Azure.AI.Projects;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task AddAgentToolsAsync(Credentials credentials, string agentId, IList<ITool> tools)
    {
        var client = CreateAgentsClient(credentials);
        
        var toolDefinitions = new List<ToolDefinition>();
        var toolResources = new ToolResources();
        
        (toolDefinitions, toolResources) = AddDocumentTools(
            tools,
            toolDefinitions,
            toolResources);
        
        (toolDefinitions, toolResources) = AddAzureFunctionsTools(
            tools,
            toolDefinitions,
            toolResources);
        
        await client.UpdateAgentAsync(
            agentId,
            tools: toolDefinitions,
            toolResources: toolResources);
    }

    private (List<ToolDefinition> toolDefinitions, ToolResources toolResources) AddAzureFunctionsTools(
        IList<ITool> tools, 
        List<ToolDefinition> toolDefinitions, 
        ToolResources toolResources)
    {
        var azureFunctionTools = tools
            .OfType<AzureFunctionTool>()
            .ToList();
        
        if (azureFunctionTools.Count == 0)
        {
            return (toolDefinitions, toolResources);
        }
        
        if (azureFunctionTools.Count > 1)
        {
            throw new ArgumentException("Only one azure function tool is allowed.");
        }

        var azureFunctionTool = azureFunctionTools.First();
        
        var azureFunctionToolDefinition = new AzureFunctionToolDefinition(
            "SendEmail",
            "Send an email",
            new AzureFunctionBinding(new AzureFunctionStorageQueue(azureFunctionTool.StorageAccountConnectionString, azureFunctionTool.InputQueueName)),
            new AzureFunctionBinding(new AzureFunctionStorageQueue(azureFunctionTool.StorageAccountConnectionString, azureFunctionTool.OutputQueueName)),
            BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new
                    {
                        Email = new
                        {
                            Type = "string",
                            Description = "The sender email address",
                        },
                        Subject = new
                        {
                            Type = "string",
                            Description = "The subject of the email",
                        },
                        Body = new
                        {
                            Type = "string",
                            Description = "The body of the email",
                        },
                    },
                    Required = new[] { "email", "subject", "body" },
                },
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
            );
        
        toolDefinitions.Add(azureFunctionToolDefinition);
        
        return (toolDefinitions, toolResources);
    }

    private (List<ToolDefinition> toolDefinitions, ToolResources toolResources) AddDocumentTools(
        IList<ITool> tools, 
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