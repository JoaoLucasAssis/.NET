using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Areas.Inventory.Models;
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
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Client> Client { get; set; } = default!;
    public DbSet<Product> Product { get; set; } = default!;
    public DbSet<Item> Item { get; set; } = default!;
    public DbSet<Order> Order { get; set; } = default!;
    public DbSet<Stock> Stock { get; set; } = default!;
    public DbSet<Store> Store { get; set; } = default!;
}
