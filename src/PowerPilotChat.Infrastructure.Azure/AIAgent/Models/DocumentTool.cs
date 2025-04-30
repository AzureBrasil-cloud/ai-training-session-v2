namespace PowerPilotChat.Infrastructure.Azure.AIAgent.Models;

public record DocumentTool(string VectorStoreId) : ITool;