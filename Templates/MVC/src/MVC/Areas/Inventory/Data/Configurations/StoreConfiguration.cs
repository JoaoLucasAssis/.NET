using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Areas.Inventory.Models;

namespace MVC.Areas.Inventory.Data.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(e => e.Location).HasColumnType("VARCHAR(120)").IsRequired();

        builder.HasMany(e => e.Stocks).WithOne(e => e.Store).OnDelete(DeleteBehavior.Cascade);
    }
}