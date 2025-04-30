namespace PowerPilotChat.Data.Entities.Interfaces;

public interface IConnector
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }
}