using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Models;

namespace MVC.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Qty).HasDefaultValue(1).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.Discount).IsRequired();
    }
}
