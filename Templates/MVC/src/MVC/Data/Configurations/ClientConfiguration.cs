using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Models;

namespace MVC.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(e => e.Phone).HasColumnType("CHAR(11)");
        builder.Property(e => e.CEP).HasColumnType("CHAR(8)").IsRequired();
        builder.Property(e => e.State).IsRequired();
        builder.Property(e => e.City).HasMaxLength(60).IsRequired();

        builder.HasIndex(e => e.Phone).HasDatabaseName("idx_client_phone");
    }
}
