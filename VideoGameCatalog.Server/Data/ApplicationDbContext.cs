using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Server.Models;

namespace VideoGameCatalog.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollectionItem>()
            .HasIndex(x => new { x.Title, x.PlatformId, x.Ownership })
            .IsUnique();
    }

    public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();
    public DbSet<Platform> Platforms => Set<Platform>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<Genre> Genres => Set<Genre>();
}