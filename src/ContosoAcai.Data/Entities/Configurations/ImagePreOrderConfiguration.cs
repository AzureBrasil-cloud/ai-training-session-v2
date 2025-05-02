using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoAcai.Data.Entities.Configurations;

public class ImagePreOrderConfiguration : IEntityTypeConfiguration<ImagePreOrder>
{
    public void Configure(EntityTypeBuilder<ImagePreOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.UserEmail)
            .IsRequired();

        builder
            .Property(x => x.ImageName)
            .IsRequired(); 
        
        builder
            .Property(x => x.ImageExtension)
            .IsRequired(); 
        
        builder
            .Property(x => x.KeyValuePairs)
            .IsRequired(); 
    }
}