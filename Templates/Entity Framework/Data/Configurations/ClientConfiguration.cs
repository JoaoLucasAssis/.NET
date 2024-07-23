using Entity_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity_Framework.Data.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(e => e.Phone).HasColumnType("CHAR(11)");
            builder.Property(e => e.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(e => e.State).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(e => e.City).HasMaxLength(60).IsRequired();

            builder.HasIndex(e => e.Phone).HasDatabaseName("idx_client_phone");
        }
    }
}
