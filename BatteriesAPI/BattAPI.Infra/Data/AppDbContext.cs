using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace BattAPI.Infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public const string DiscriminatorProperty = "Discriminator";

        public DbSet<User> Users => Set<User>();

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Battery> Batteries => Set<Battery>();

        public DbSet<Note> Notes => Set<Note>();

        public DbSet<ProductImageMeta> ProductImages => Set<ProductImageMeta>();
        public DbSet<NoteAttachmentMeta> NoteAttachments => Set<NoteAttachmentMeta>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<FileMeta>().UseTphMappingStrategy();

            modelBuilder.Entity<Product>().UseTptMappingStrategy()
                .Property<string>(DiscriminatorProperty)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ImageMeta)
                .WithOne(img => img.Product)
                .HasForeignKey<ProductImageMeta>(img => img.ProductId);

            modelBuilder.Entity<Battery>().OwnsOne(b => b.Specs);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Attachments)
                .WithOne(a => a.Note)
                .HasForeignKey(a => a.NoteId);
        }

        public override int SaveChanges()
        {
            SetTimestamps();
            SetDiscriminators();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();
            SetDiscriminators();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.Updated = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Updated = DateTime.UtcNow;
                }
            }
        }

        private void SetDiscriminators()
        {
            foreach (var entry in ChangeTracker.Entries<Product>())
            {
                if (entry.State == EntityState.Added)
                    entry.Property<string>(DiscriminatorProperty).CurrentValue = entry.Entity.GetType().Name;
            }
        }
    }
}
