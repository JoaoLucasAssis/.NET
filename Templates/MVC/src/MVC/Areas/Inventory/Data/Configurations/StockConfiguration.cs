using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Areas.Inventory.Models;

namespace MVC.Areas.Inventory.Data.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Qty).HasDefaultValue(1).IsRequired(); 

        builder.HasMany(e => e.Products).WithOne(e => e.Stock).OnDelete(DeleteBehavior.Cascade);
    }
}
