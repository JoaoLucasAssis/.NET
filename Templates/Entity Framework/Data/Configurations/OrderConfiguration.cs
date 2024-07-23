using Entity_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity_Framework.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.StartDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(e => e.Status).HasConversion<string>();
            builder.Property(e => e.Observation).HasColumnType("VARCHAR(512)");

            builder.HasMany(e => e.Items).WithOne(e => e.Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
