
using Articles.Domain.Entities;
using Articles.Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Data;

public class AppDbContext : DbContext
{
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      public DbSet<Article> Articles { get; set; }
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

                  // Índice único para el nombre
                  entity.HasIndex(e => e.Name).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
      }
}
