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
        builder.Property(e => e.Barcode).HasColumnType("VARCHAR(14)").IsRequired();
        builder.Property(e => e.Description).HasColumnType("VARCHAR(60)");
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.ProductType).HasConversion<string>();
    }
}
