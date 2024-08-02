using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Models;

namespace MVC.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(e => e.Description).HasColumnType("VARCHAR(60)");
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.ProductType).HasConversion<string>();

        builder.Ignore(e => e.Qty);
        builder.Ignore(e => e.IsAvailable);
    }
}
