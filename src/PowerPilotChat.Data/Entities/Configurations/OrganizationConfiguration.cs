using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PowerPilotChat.Data.Entities.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.PersonalIdentifier)
            .IsRequired();
        
        builder
            .Property(x => x.LegalName)
            .IsRequired();
        
        builder
            .Property(x => x.FantasyName)
            .IsRequired();
        
        builder
            .Property(x => x.TenantId)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
    }
}