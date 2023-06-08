using Microsoft.EntityFrameworkCore;
using ProductsApp.Domain.Entities;

namespace ProductsApp.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasConversion<string>();

                entity.Property(p => p.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18, 2)")
                    .IsRequired()
                    .HasAnnotation("CheckConstraint", "Price > 0"); 

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

            });
        }
    }
}
