using Microsoft.EntityFrameworkCore;
using PowerPilotChat.Data.Entities;

namespace PowerPilotChat.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            var p = item.FindPrimaryKey()?.Properties.FirstOrDefault(i=>i.ValueGenerated!=Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
            if (p!=null)
            {
                p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
            }
        }
    }
}