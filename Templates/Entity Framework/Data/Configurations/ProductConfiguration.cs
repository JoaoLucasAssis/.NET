using Entity_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity_Framework.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
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
}
