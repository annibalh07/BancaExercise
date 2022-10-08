using BpInterface.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BpInterface.Infrastructure.Data.Config
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Contrasenia)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            builder.Property(e => e.Identificacion)
                    .HasMaxLength(13)
                    .IsUnicode(false);

            builder.Property(e => e.Nombres)
                    .HasMaxLength(150)
                    .IsUnicode(false);

            builder.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);

            builder.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            builder.Property(e => e.Genero)
                    .HasMaxLength(15)
                    .IsUnicode(false);

            builder.Property(e => e.Estado)
                    .HasDefaultValueSql("((1))");

            builder.HasIndex(e => e.Identificacion, "UNC_Cliente_Identificacion_Include")
                    .IsUnique()
                    .HasFillFactor(90);
        }
    }
}
