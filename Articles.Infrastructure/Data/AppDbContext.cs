
using Articles.Domain.Entities;
using Articles.Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Data;

public class AppDbContext : DbContext
{
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      public DbSet<Article> Articles { get; set; }
      public DbSet<Category> Categories { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            // Configuración de Article
            modelBuilder.Entity<Article>(entity =>
            {
                  entity.HasKey(e => e.Id);

                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasConversion(ArticleConverters.NameConverter)
                        .HasMaxLength(100);

                  entity.Property(e => e.Price)
                        .HasConversion(ArticleConverters.PriceConverter)
                        .IsRequired();

                  entity.Property(e => e.Description)
                        .IsRequired()
                        .HasConversion(ArticleConverters.DescriptionConverter)
                        .HasMaxLength(500);

                  entity.Property(e => e.CreatedAt)
                        .IsRequired();

                  entity.Property(e => e.UpdatedAt)
                        .IsRequired();

                  entity.Property(e => e.CategoryId)
                        .IsRequired();
                  // Índice único para el nombre
                  entity.HasIndex(e => e.Name).IsUnique();
                  // Relación muchos a uno con Category
                  entity.HasOne(a => a.Category)
                        .WithMany(c => c.Articles)
                        .HasForeignKey(a => a.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada
            });
            // Configuración de Category
            modelBuilder.Entity<Category>(entity =>
            {
                  entity.HasKey(e => e.Id);
                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasConversion(CategoryConverters.NameConverter)
                        .HasMaxLength(100);

                  entity.Property(e => e.CreatedAt)
                        .IsRequired();

                  entity.Property(e => e.UpdatedAt)
                        .IsRequired();

                  // Índice único para el nombre
                  entity.HasIndex(e => e.Name).IsUnique();
                  // Relación uno a muchos con Articles
                  entity.HasMany(c => c.Articles)
                        .WithOne(a => a.Category)
                        .HasForeignKey(a => a.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada
            });
            base.OnModelCreating(modelBuilder);
      }
}
