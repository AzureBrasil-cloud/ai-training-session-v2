namespace PowerPilotChat.Infrastructure.Azure.ResourceManager.Models;

public record Deployment(
    string Id, 
    string Name, 
    string? Status, // Succeeded = ok
    string? ErrorMessage);
    