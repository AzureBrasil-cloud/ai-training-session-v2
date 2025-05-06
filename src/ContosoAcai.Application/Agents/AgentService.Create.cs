using ContosoAcai.Application.Agents.Models.Requests;
using ContosoAcai.Application.Agents.Models.Results;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;
using PowerPilotChat.Application;
using File = ContosoAcai.Infrastructure.AIAgent.Models.File;

namespace ContosoAcai.Application.Agents;

public partial class AgentService
{
    public async Task<Result<AgentResult>> CreateAsync(CreateAgentRequest request)
    {
        var agent = await CreateAgentAsync(request);

        await AddToolsAsync(agent.Id, request.Type);
        
        return new AgentResult(agent.Id);
    }

    private async Task AddToolsAsync(string agentId, AgentType agentType)
    {
        IList<ITool> tools = new List<ITool>();
        
        if (agentType == AgentType.Guide)
        {
            var guideFile = await UploadFileAsync("Guide.txt", Constants.ConstosoAcaiDocument);
            var vectorStore = await CreateVectorStoreAsync(guideFile.Id);
        
            tools.Add(new DocumentTool(vectorStore.Id));
        }
        
        if (agentType == AgentType.Sales)
        {
            // var salesFile = await UploadFileAsync("Sales.txt", Constants.ConstosoAcaiDocument);
            // var vectorStore = await CreateVectorStoreAsync(salesFile.Id);
            //
            // tools.Add(new DocumentTool(vectorStore.Id));
        }
        
        await aiAgentService.AddAgentToolsAsync(
            CreateCredentials(),
            agentId, 
            tools);
    }

    private async Task<VectorStore> CreateVectorStoreAsync(string fileId)
    {
        return await aiAgentService.CreateVectorStoreAsync(
            CreateCredentials(),
            Guid.NewGuid().ToString(),
            [fileId]);
    }

    private async Task<File> UploadFileAsync(string name, string content)
    {
        using var contentStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        
        return await aiAgentService.UploadFileAsync(
            CreateCredentials(),
            contentStream,
            name);
    }

    private async Task<Agent> CreateAgentAsync(CreateAgentRequest request)
    {
        return await aiAgentService.CreateAgentAsync(
            CreateCredentials(),
            $"agent-{Guid.NewGuid()}",
            request.Type == AgentType.Guide ? Constants.GuideAgentInstructions : Constants.SalesAgentInstructions,
            configuration["AI:Agent:Model"]!);
    }
}