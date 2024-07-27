using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // The line below automatically applies all configurations applied above.
        // It applies to all classes that implement IEntityTypeConfiguration<T>
        // found in the specified assembly.

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

public DbSet<MVC.Models.Client> Client { get; set; } = default!;

public DbSet<MVC.Models.Product> Product { get; set; } = default!;

public DbSet<MVC.Models.Item> Item { get; set; } = default!;

public DbSet<MVC.Models.Order> Order { get; set; } = default!;
}
