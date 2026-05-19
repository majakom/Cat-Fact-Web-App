using Microsoft.EntityFrameworkCore;
using CatFact.Infrastructure.Persistence.Models;

namespace CatFact.Infrastructure.Persistence;

public class CatFactDbContext : DbContext
{
    public CatFactDbContext(DbContextOptions<CatFactDbContext> options)
        : base(options) { }
    public DbSet<CatFactDbEntity> CatFacts => Set<CatFactDbEntity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatFactDbEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Fact)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(x => x.Length)
                .IsRequired();
        });
    }
}