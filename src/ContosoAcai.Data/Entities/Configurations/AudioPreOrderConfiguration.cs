using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoAcai.Data.Entities.Configurations;

public class AudioPreOrderConfiguration : IEntityTypeConfiguration<AudioPreOrder>
{
    public void Configure(EntityTypeBuilder<AudioPreOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.UserEmail)
            .IsRequired();

        builder
            .Property(x => x.AudioName)
            .IsRequired();

        builder
            .Property(x => x.AudioExtension)
            .IsRequired();

        builder
            .Property(x => x.Content)
            .IsRequired();
    }
}