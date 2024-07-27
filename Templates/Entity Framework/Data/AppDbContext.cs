using Entity_Framework.Data.Configurations;
using Entity_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Data;

internal class AppDbContext : DbContext
{
    // You can tell EF which entity you want
    // to create in the data model in two ways

    // First way
    public DbSet<Order> Orders {  get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TemplateEF;Trusted_Connection=True;");
    }

    // Second way
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // FluentAPI was used to configure each data model
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());

        // The comment line below automatically applies all configurations applied above.
        // It applies to all classes that implement IEntityTypeConfiguration<T found in the specified assembly.

        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
