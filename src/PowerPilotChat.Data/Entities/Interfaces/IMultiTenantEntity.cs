namespace PowerPilotChat.Data.Entities.Interfaces;

public interface IMultiTenantEntity : IEntity
{
    public Guid OrganizationId { get; set; }
}