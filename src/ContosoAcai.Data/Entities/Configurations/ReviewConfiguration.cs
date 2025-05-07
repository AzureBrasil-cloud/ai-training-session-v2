using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoAcai.Data.Entities.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.UserEmail)
            .IsRequired();

        builder
            .Property(x => x.Content)
            .IsRequired();

        builder
            .Property(x => x.Classification)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
    }
}