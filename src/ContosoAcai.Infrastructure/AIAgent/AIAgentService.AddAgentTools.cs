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
        
        (toolDefinitions, toolResources) = AddEmailTools(
            tools,
            toolDefinitions,
            toolResources);
        
        await client.UpdateAgentAsync(
            agentId,
            tools: toolDefinitions,
            toolResources: toolResources);
    }

    private (List<ToolDefinition> toolDefinitions, ToolResources toolResources) AddEmailTools(
        IList<ITool> tools, 
        List<ToolDefinition> toolDefinitions, 
        ToolResources toolResources)
    {
        var emailTools = tools
            .OfType<EmailTool>()
            .ToList();
        
        if (emailTools.Count == 0)
        {
            return (toolDefinitions, toolResources);
        }
        
        if (emailTools.Count > 1)
        {
            throw new ArgumentException("Only one email tool is allowed.");
        }

        var emailTool = emailTools.First();
        
        FunctionToolDefinition emailToolDefinition = new(
            name: nameof(EmailTool),
            description: "Send email to a user",
            parameters: BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new
                    {
                        receiver = new
                        {
                            Type = "string",
                            Description = "The email of the receiver",
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
                    Required = new[] { "receiver", "Subject", "body"},
                },
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        
        toolDefinitions.Add(emailToolDefinition);
        
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