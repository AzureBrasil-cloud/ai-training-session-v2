using System.Text.Json;
using Azure.AI.Projects;
using ContosoAcai.Infrastructure.Azure.Shared;
using Agent = ContosoAcai.Infrastructure.Azure.AIAgent.Models.Agent;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<Agent> CreateAgentAsync(
        Credentials credentials, 
        string name,
        string instructions,
        string aiModel)
    {
        var client = CreateAgentsClient(credentials);
        
        var azureFunctionToolDefinition = new AzureFunctionToolDefinition(
            "SendEmail",
            "Send an email",
            new AzureFunctionBinding(new AzureFunctionStorageQueue("https://stcontosoacaiweastus3.queue.core.windows.net", "input")),
            new AzureFunctionBinding(new AzureFunctionStorageQueue("https://stcontosoacaiweastus3.queue.core.windows.net", "output")),
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
        
        var agentResponse = await client.CreateAgentAsync(
            model: aiModel,
            name: name,
            instructions: instructions,
            tools:
            [
                azureFunctionToolDefinition
            ]);

        return new Agent(
            agentResponse.Value.Id,
            agentResponse.Value.Name,
            agentResponse.Value.Instructions);
    }
}