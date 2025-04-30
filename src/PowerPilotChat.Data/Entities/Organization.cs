using System.Diagnostics.CodeAnalysis;
using PowerPilotChat.Common;
using PowerPilotChat.Data.Entities.Interfaces;

namespace PowerPilotChat.Data.Entities;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public class Organization : IEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string FantasyName { get; set; } = null!;
    public string LegalName { get; set; } = null!;
    public string PersonalIdentifier { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    //EF
    public Organization()
    {}
    
    public Organization(

        Guid tenantId, 
        string fantasyName, 
        string legalName,
        string personalIdentifier)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        LegalName = legalName.Trim();
        FantasyName = fantasyName.Trim();
        PersonalIdentifier = personalIdentifier.CleanPersonalIdentifier();
    }
    
    public void Update(string requestFantasyName, string requestLegalName, string requestPersonalIdentifier)
    {
        FantasyName = requestFantasyName.Trim();
        LegalName = requestLegalName.Trim();
        PersonalIdentifier = requestPersonalIdentifier.CleanPersonalIdentifier();
    }
}