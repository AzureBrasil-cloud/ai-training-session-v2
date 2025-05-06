namespace ContosoAcai.Infrastructure.Azure.AIAgent.Models;

public record AzureFunctionTool(
    string StorageAccountConnectionString,
    string InputQueueName,
    string OutputQueueName) : ITool;
