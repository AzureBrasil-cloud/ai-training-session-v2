namespace ContosoAcai.Infrastructure.AIAgent.Models;

public record AzureFunctionTool(
    string StorageAccountConnectionString,
    string InputQueueName,
    string OutputQueueName) : ITool;
