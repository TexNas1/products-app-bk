using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsApp.Domain.Entities;

namespace ProductsApp.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ProductDbContext()
        {
            // This constructor is required for EF Core design-time operations.
        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ProductsDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("uuid_generate_v4()");

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
