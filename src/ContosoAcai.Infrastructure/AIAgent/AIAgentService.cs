using System.Text.Json;
using Azure.AI.Projects;
using Azure.Identity;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;
using ContosoAcai.Infrastructure.Email;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService(EmailService emailService)
{
    private AgentsClient CreateAgentsClient(Credentials credentials)
    {
        var credential = new ClientSecretCredential(
            credentials.TenantId, 
            credentials.ClientId, 
            credentials.ClientSecret);
        
        return new AgentsClient(credentials.ProjectConnectionString, credential);
    }
    
    private async Task<ToolOutput> GetResolvedToolOutputAsync(RequiredToolCall toolCall)
    {
        if (toolCall is not RequiredFunctionToolCall functionToolCall) return null!;
        
        using var argumentsJson = JsonDocument.Parse(functionToolCall.Arguments);
            
        if (functionToolCall.Name == nameof(EmailTool))
        {
            string receiver = argumentsJson.RootElement.GetProperty("receiver").GetString()!;
            string subject = argumentsJson.RootElement.GetProperty("subject").GetString()!;
            string body = argumentsJson.RootElement.GetProperty("body").GetString()!;
                
            var result = await emailService.SendAsync(receiver, subject, body);
                
            return new ToolOutput(toolCall, result);
        }

        return null!;
    }
}