using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoAcai.Data.Entities.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.UserEmail)
            .IsRequired();

        builder
            .Property(x => x.Size)
            .IsRequired();

        builder
            .Property(x => x.Extras)
            .IsRequired();

        builder
            .Property(x => x.TotalValue)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
    }
}