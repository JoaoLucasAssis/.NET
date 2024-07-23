using Entity_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity_Framework.Data.Configurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
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
}
